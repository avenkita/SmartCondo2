using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WindowsFormsXmlTransformation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\Aishwarya\Desktop\SmartCondo\ConversionXML\simulationWorld.xml");
            while ((line = file.ReadLine()) != null)
            {
                listBox1.Items.Add(line);
            }
            file.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //listBox2.Items.Add(listBox1.Items[0] + "Something is wrong");
            //if (listBox1.Items[2].Equals(" <actions>")) { listBox2.Items.Add("Deactivated"); }
            //listBox2.Items.Add(listBox1.Items[2]);

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                //listBox1.Items[i] is current line
                //listBox1.Items[i + 1] is next line 
                string currentline = listBox1.Items[i].ToString().Trim();
                if (currentline.Equals("<room>") || currentline.Equals("<obstacle>"))
                {
                    if ((listBox1.Items[i + 1].ToString().Trim().Equals("<wall>")) /*&& (listBox1.Items[i+1].ToString().Trim().Equals("<point>80 0</point>"))*/)
                    {
                        int j = i;
                        //listBox2.Items.Add(listBox1.Items[j])
                        while (listBox1.Items[j].ToString().Trim() != "</wall>")
                        //problem potentially: will only get the first wall tag and its point stuff!! The if statement does not ensure it loops through all the walls in a room
                        {
                            string thisline = listBox1.Items[j].ToString().Trim();
                            if (thisline == "<door>") { continue; }
                            listBox2.Items.Add(listBox1.Items[j]); j += 1; //remember to increment! 
                        }
                        listBox2.Items.Add((listBox1.Items[j]));

                        //listBox2.Items.Add(listBox1.Items[i+2]);
                        //listBox2.Items.Add(listBox1.Items[i+3]);
                    }
                }
            }
            XmlTextWriter writer = new XmlTextWriter("C:\\Users\\Aishwarya\\Desktop\\SmartCondo\\ConversionXML\\walltags.xml", Encoding.UTF8); //notice you must specify the path with \\ double backslashes
            writer.WriteString("<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"no\"?> \n");
            for (int k = 0; k < listBox2.Items.Count; k++)
            {
                writer.WriteRaw(listBox2.Items[k].ToString() + "\n");
                writer.Formatting = Formatting.Indented;
            }


        }
    }
}
