﻿#pragma checksum "..\..\..\HomophonicPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "81FE535E39100FB5546888A3AC85D4BE020DB1D1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Cryptex;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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
using System.Windows.Shell;


namespace Cryptex {
    
    
    /// <summary>
    /// HomophonicPage
    /// </summary>
    public partial class HomophonicPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\..\HomophonicPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel row1;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\HomophonicPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel row2;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\HomophonicPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox plainTextInputTextBox;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\..\HomophonicPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button setSubstitutionsButton;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\HomophonicPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button encryptButton;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\HomophonicPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox outputTextBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.8.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Cryptex;component/homophonicpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\HomophonicPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.8.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.row1 = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 2:
            this.row2 = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 3:
            this.plainTextInputTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.setSubstitutionsButton = ((System.Windows.Controls.Button)(target));
            
            #line 92 "..\..\..\HomophonicPage.xaml"
            this.setSubstitutionsButton.Click += new System.Windows.RoutedEventHandler(this.setSubstitutionsButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.encryptButton = ((System.Windows.Controls.Button)(target));
            
            #line 93 "..\..\..\HomophonicPage.xaml"
            this.encryptButton.Click += new System.Windows.RoutedEventHandler(this.encryptButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.outputTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

