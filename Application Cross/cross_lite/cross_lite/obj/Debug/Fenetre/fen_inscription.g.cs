#pragma checksum "..\..\..\Fenetre\fen_inscription.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "D918808F7EEAE2DA3B42FBE8C0AC45FB2F307D10010CD0E3209671894A085949"
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
    /// fen_inscription
    /// </summary>
    public partial class fen_inscription : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\Fenetre\fen_inscription.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cb_classe;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Fenetre\fen_inscription.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lb_eleve;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Fenetre\fen_inscription.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_inscrire;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Fenetre\fen_inscription.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtbox_dossard;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Fenetre\fen_inscription.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtbox_tag;
        
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
            System.Uri resourceLocater = new System.Uri("/cross_lite;component/fenetre/fen_inscription.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Fenetre\fen_inscription.xaml"
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
            this.cb_classe = ((System.Windows.Controls.ComboBox)(target));
            
            #line 11 "..\..\..\Fenetre\fen_inscription.xaml"
            this.cb_classe.Loaded += new System.Windows.RoutedEventHandler(this.Cb_classe_Loaded);
            
            #line default
            #line hidden
            
            #line 11 "..\..\..\Fenetre\fen_inscription.xaml"
            this.cb_classe.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Cb_classe_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.lb_eleve = ((System.Windows.Controls.ListBox)(target));
            
            #line 13 "..\..\..\Fenetre\fen_inscription.xaml"
            this.lb_eleve.PreviewMouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.Lb_eleve_PreviewMouseDoubleClick);
            
            #line default
            #line hidden
            
            #line 13 "..\..\..\Fenetre\fen_inscription.xaml"
            this.lb_eleve.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Lb_eleve_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btn_inscrire = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\Fenetre\fen_inscription.xaml"
            this.btn_inscrire.Click += new System.Windows.RoutedEventHandler(this.Btn_inscrire_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.txtbox_dossard = ((System.Windows.Controls.TextBox)(target));
            
            #line 17 "..\..\..\Fenetre\fen_inscription.xaml"
            this.txtbox_dossard.MouseEnter += new System.Windows.Input.MouseEventHandler(this.Txtbox_dossard_MouseEnter);
            
            #line default
            #line hidden
            return;
            case 5:
            this.txtbox_tag = ((System.Windows.Controls.TextBox)(target));
            
            #line 18 "..\..\..\Fenetre\fen_inscription.xaml"
            this.txtbox_tag.MouseEnter += new System.Windows.Input.MouseEventHandler(this.Txtbox_tag_MouseEnter);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

