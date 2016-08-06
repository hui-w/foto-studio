using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Collections;
using System.IO;
using System.Windows.Threading;
using QLike.Foto.Common;

namespace QLike.Foto.GridStudio.Controls
{
    /// <summary>
    /// Interaction logic for ThunmContainer.xaml
    /// </summary>
    public partial class ThumbContainer : UserControl
    {
        public delegate void MessageHandler(object sender, string e);
        public event MessageHandler onMessage;

        private static Thread executingThread = null;
        private TipBoard tipBoard = null;

        /// <summary>
        /// Identifies the Value dependency property.
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty =
          DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(ThumbContainer),
          new FrameworkPropertyMetadata(new PropertyChangedCallback(OnItemChanged)));

        /// <summary>
        /// Identifies the ValueChanged routed event.
        /// </summary>
        public static readonly RoutedEvent ItemChangedEvent = EventManager.RegisterRoutedEvent(
            "ItemChanged", RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventArgs<IEnumerable>), typeof(ThumbContainer)
            );

        public ThumbContainer()
        {
            InitializeComponent();
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        private static void OnItemChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ThumbContainer me = obj as ThumbContainer;
            if (me != null)
            {
                me.stackPanelMain.Children.Clear();
                me.ShowTip(string.Empty);

                if (me.ItemsSource == null)
                {
                    me.ShowTip("Source N/A");
                    return;
                }

                IList<FileInfo> files = new List<FileInfo>();

                //Calc the count
                foreach (FileInfo file in me.ItemsSource)
                {
                    files.Add(file);
                }

                if (files.Count == 0)
                {
                    //Show no thumb
                    me.ShowTip("No Image");
                }
                else
                {
                    //Clear old items
                    me.stackPanelMain.Children.Clear();

                    //Show thumb
                    if (executingThread != null && executingThread.IsAlive)
                    {
                        executingThread.Abort();
                    }
                    executingThread = new Thread(new ParameterizedThreadStart(me.ShowThumbItems));
                    executingThread.IsBackground = true;
                    executingThread.Start(files);
                }
            }
        }

        /// <summary>
        /// Thread work method
        /// </summary>
        private void ShowThumbItems(object obj)
        {
            IList<FileInfo> files = obj as List<FileInfo>;
            for (int i = 0; i < files.Count; i++)
            {
                FileInfo file = files[i];
                this.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate()
                {
                    this.ShowMessage(string.Format("{0}% Loaded; Loading {1} of {2}...",
                        (int)((double)i / (double)files.Count * 100),
                        i, files.Count));

                    ImageThumb imageThumb = new ImageThumb(120, 80, file.FullName);
                    imageThumb.Margin = new Thickness(2);
                    imageThumb.Cursor = Cursors.Hand;
                    imageThumb.ToolTip += string.Format("{0} of {1}\n{2} ({3})\n{4}", 
                        i + 1,
                        files.Count,
                        file.Name,
                        Utility.FormatFileSize(file.Length),
                        "This image can be dragged into the cell");
                    this.stackPanelMain.Children.Add(imageThumb);
                });

                System.Threading.Thread.Sleep(100);
            }

            //Remove processing
            this.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate()
            {
                this.ShowMessage(string.Format("{0} Image(s) Loaded", files.Count));
            });
        }

        public void ShowTip(string tipContent)
        {
            if (string.IsNullOrEmpty(tipContent))
            {
                if (this.tipBoard != null)
                {
                    this.stackPanelMain.Children.Remove(this.tipBoard);
                    this.tipBoard = null;
                }
            }
            else
            {
                if (this.tipBoard == null)
                {
                    this.tipBoard = new TipBoard(tipContent);
                    this.stackPanelMain.Children.Add(this.tipBoard);
                }
                else
                {
                    this.tipBoard.TipContent = tipContent;
                }
            }
        }

        private void ShowMessage(string message)
        {
            if (onMessage != null)
            {
                onMessage(this, message);
            }
        }
    }//end of c;ass
}
