﻿#pragma checksum "..\..\..\Views\AllPC.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "AF56F46C3F9E10DEC422D3473FDA9550"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro.Controls;
using PC.DataAccess;
using PC.Utils;
using PC.ViewModels;
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
using System.Windows.Shell;


namespace PC.Views {
    
    
    /// <summary>
    /// AllPC
    /// </summary>
    public partial class AllPC : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 33 "..\..\..\Views\AllPC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MahApps.Metro.Controls.ToggleSwitch btn_toggle_edit;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\Views\AllPC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnUpdate;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\Views\AllPC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnDelete;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\Views\AllPC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnExport;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\Views\AllPC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid pcDataGrid;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\..\Views\AllPC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn pC_NameColumn;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\Views\AllPC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn nVColumn;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\Views\AllPC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn nVCodeColumn;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\Views\AllPC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn pBColumn;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\Views\AllPC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn iPColumn;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\Views\AllPC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn mACColumn;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\..\Views\AllPC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn mAC2Column;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\..\Views\AllPC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn office_LocatedColumn;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\Views\AllPC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn cPUColumn;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\..\Views\AllPC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn hDDColumn;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\..\Views\AllPC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn oSColumn;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\..\Views\AllPC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn rAMColumn;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\Views\AllPC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn typeColumn;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\..\Views\AllPC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn serviceTagColumn;
        
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
            System.Uri resourceLocater = new System.Uri("/PC;component/views/allpc.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\AllPC.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            
            #line 12 "..\..\..\Views\AllPC.xaml"
            ((PC.Views.AllPC)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btn_toggle_edit = ((MahApps.Metro.Controls.ToggleSwitch)(target));
            
            #line 37 "..\..\..\Views\AllPC.xaml"
            this.btn_toggle_edit.Checked += new System.EventHandler<System.Windows.RoutedEventArgs>(this.BtnEditChecked);
            
            #line default
            #line hidden
            
            #line 37 "..\..\..\Views\AllPC.xaml"
            this.btn_toggle_edit.Unchecked += new System.EventHandler<System.Windows.RoutedEventArgs>(this.BtnEditUnchecked);
            
            #line default
            #line hidden
            return;
            case 3:
            this.BtnUpdate = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\..\Views\AllPC.xaml"
            this.BtnUpdate.Click += new System.Windows.RoutedEventHandler(this.BtnUpdateClicked);
            
            #line default
            #line hidden
            return;
            case 4:
            this.BtnDelete = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\..\Views\AllPC.xaml"
            this.BtnDelete.Click += new System.Windows.RoutedEventHandler(this.BtnDeleteClicked);
            
            #line default
            #line hidden
            return;
            case 5:
            this.BtnExport = ((System.Windows.Controls.Button)(target));
            
            #line 54 "..\..\..\Views\AllPC.xaml"
            this.BtnExport.Click += new System.Windows.RoutedEventHandler(this.BtnExport_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.pcDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 68 "..\..\..\Views\AllPC.xaml"
            this.pcDataGrid.RowEditEnding += new System.EventHandler<System.Windows.Controls.DataGridRowEditEndingEventArgs>(this.PcViewSource_RowEditEnding);
            
            #line default
            #line hidden
            return;
            case 7:
            this.pC_NameColumn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 8:
            this.nVColumn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 9:
            this.nVCodeColumn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 10:
            this.pBColumn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 11:
            this.iPColumn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 12:
            this.mACColumn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 13:
            this.mAC2Column = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 14:
            this.office_LocatedColumn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 15:
            this.cPUColumn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 16:
            this.hDDColumn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 17:
            this.oSColumn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 18:
            this.rAMColumn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 19:
            this.typeColumn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 20:
            this.serviceTagColumn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

