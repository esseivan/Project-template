﻿using EsseivaN.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_template
{
    public partial class frmMain : Form
    {
        public Logger LogManager { get; set; }

        public frmMain(Logger LogManager)
        {
            InitializeComponent();
            this.LogManager = LogManager;
        }
    }
}
