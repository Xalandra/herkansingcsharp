﻿#pragma checksum "D:\Software\GitHub Desktop\herkansingcsharp\Vidarr\pgDownload.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D05540C1478746C55405D6EF0A2C1591"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vidarr
{
    partial class pgDownload : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.gridRoot = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 2:
                {
                    this.Smartphone = (global::Windows.UI.Xaml.VisualState)(target);
                }
                break;
            case 3:
                {
                    this.Tablet = (global::Windows.UI.Xaml.VisualState)(target);
                }
                break;
            case 4:
                {
                    this.Desktop = (global::Windows.UI.Xaml.VisualState)(target);
                }
                break;
            case 5:
                {
                    this.DownloadslistLabel = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 6:
                {
                    this.lstDownload = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    #line 209 "..\..\..\pgDownload.xaml"
                    ((global::Windows.UI.Xaml.Controls.ListView)this.lstDownload).LayoutUpdated += this.lstDownload_LayoutUpdated;
                    #line default
                }
                break;
            case 7:
                {
                    this.GoToConverterButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 283 "..\..\..\pgDownload.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.GoToConverterButton).Click += this.goToConverterScreen;
                    #line default
                }
                break;
            case 8:
                {
                    this.DescriptionLabelDownloader = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 9:
                {
                    this.SearchBox = (global::Windows.UI.Xaml.Controls.AutoSuggestBox)(target);
                    #line 112 "..\..\..\pgDownload.xaml"
                    ((global::Windows.UI.Xaml.Controls.AutoSuggestBox)this.SearchBox).QuerySubmitted += this.querySubmittedZoek;
                    #line default
                }
                break;
            case 10:
                {
                    this.SearchWordResults = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 11:
                {
                    this.DownloadInMP4Button = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 199 "..\..\..\pgDownload.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.DownloadInMP4Button).Click += this.downloadSelectedMPFour;
                    #line default
                }
                break;
            case 12:
                {
                    this.DownloadInMP3Button = (global::Windows.UI.Xaml.Controls.Button)(target);
                }
                break;
            case 13:
                {
                    global::Windows.UI.Xaml.Controls.CheckBox element13 = (global::Windows.UI.Xaml.Controls.CheckBox)(target);
                    #line 179 "..\..\..\pgDownload.xaml"
                    ((global::Windows.UI.Xaml.Controls.CheckBox)element13).Checked += this.saveCheckedUrl;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

