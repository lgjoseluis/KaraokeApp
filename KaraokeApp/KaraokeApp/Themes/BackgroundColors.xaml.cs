﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KaraokeApp.Themes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BackgroundColors : ResourceDictionary
    {
        public BackgroundColors()
        {
            InitializeComponent();
        }
    }
}