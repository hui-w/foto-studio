﻿#pragma checksum "..\..\ToolWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6FE0B6C6FB60978C963C7135776B36F7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace QLike.Foto.GridStudio {
    
    
    /// <summary>
    /// ToolWindow
    /// </summary>
    public partial class ToolWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\ToolWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbColor;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\ToolWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnWinformDlg;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\ToolWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMicrosoftDlg;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\ToolWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnFolderBrowser;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\ToolWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtOutput;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/FotoGridStudio;component/toolwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ToolWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 5 "..\..\ToolWindow.xaml"
            ((QLike.Foto.GridStudio.ToolWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.cmbColor = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.btnWinformDlg = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\ToolWindow.xaml"
            this.btnWinformDlg.Click += new System.Windows.RoutedEventHandler(this.btnWinformDlg_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnMicrosoftDlg = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\ToolWindow.xaml"
            this.btnMicrosoftDlg.Click += new System.Windows.RoutedEventHandler(this.btnMicrosoftDlg_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnFolderBrowser = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\ToolWindow.xaml"
            this.btnFolderBrowser.Click += new System.Windows.RoutedEventHandler(this.btnFolderBrowser_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.txtOutput = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

