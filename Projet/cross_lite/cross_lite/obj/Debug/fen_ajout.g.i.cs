﻿#pragma checksum "..\..\fen_ajout.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "747D6A1B38984F0B6E610CEF88B4FC2BDBD52A22"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
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
using System.Windows.Shell;
using cross_lite;


namespace cross_lite {
    
    
    /// <summary>
    /// ajout_eleve
    /// </summary>
    public partial class ajout_eleve : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\fen_ajout.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton radiobtn_m;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\fen_ajout.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton radiobtn_f;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\fen_ajout.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Calendar calendar_naissance;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\fen_ajout.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtbox_classe;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\fen_ajout.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtbox_prenom;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\fen_ajout.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtbox_nom;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\fen_ajout.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_enregistrer;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\fen_ajout.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_retour;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\fen_ajout.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cb_up;
        
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
            System.Uri resourceLocater = new System.Uri("/cross_lite;component/fen_ajout.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\fen_ajout.xaml"
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
            this.radiobtn_m = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 2:
            this.radiobtn_f = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 3:
            this.calendar_naissance = ((System.Windows.Controls.Calendar)(target));
            return;
            case 4:
            this.txtbox_classe = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.txtbox_prenom = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.txtbox_nom = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.btn_enregistrer = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\fen_ajout.xaml"
            this.btn_enregistrer.Click += new System.Windows.RoutedEventHandler(this.Btn_enregistrer_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btn_retour = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\fen_ajout.xaml"
            this.btn_retour.Click += new System.Windows.RoutedEventHandler(this.Btn_retour_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.cb_up = ((System.Windows.Controls.ComboBox)(target));
            
            #line 23 "..\..\fen_ajout.xaml"
            this.cb_up.Loaded += new System.Windows.RoutedEventHandler(this.Cb_up_Loaded);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
