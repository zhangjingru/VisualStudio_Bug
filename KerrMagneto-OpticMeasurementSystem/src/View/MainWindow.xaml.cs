﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AduSkin.Controls.Metro;
//using MVVM.Model;
using MVVM.View;
using MVVM.ViewModel;
using Possible;

namespace MVVM.View
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            FuncName();
        }
        public void FuncName()
        {
            var Recipes = new List<Recipe>();

            Console.WriteLine();
            Console.WriteLine();
            var WaferTypes = Recipes.Select(item => item.WaferType).Distinct();
            //var WaferType = WaferTypes.First();
            //var TypeCount = WaferTypes.Count();
        }

    }
}
