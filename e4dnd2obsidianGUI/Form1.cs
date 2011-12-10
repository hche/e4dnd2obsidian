using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using e4dnd2obsidian;

namespace e4dnd2obsidianGUI
{
    public partial class Form1 : Form
    {
        e4dnd2obsidianProg prg;

        # region Hilfsfunktionen
        private void updateCampaignMenu()
        {
            List<campaign> cmps = prg.getCampaigns();
            string selectedCmp = prg.selectedCampaign.name;

            comboBoxCampaign.BeginUpdate();
            comboBoxCampaign.Items.Clear();
            foreach (campaign cmp in cmps)
            {
                int idx = comboBoxCampaign.Items.Add(cmp.name);
                if (cmp.name == selectedCmp)
                    comboBoxCampaign.SelectedIndex = idx;
            }
            comboBoxCampaign.EndUpdate();
        }

        private void updateCharacterMenu()
        {
            List<character> chrs = prg.getCharacters();
            string selectedChr = prg.selectedCharacter.name;

            comboBoxCharacter.BeginUpdate();
            comboBoxCharacter.Items.Clear();
            foreach (character chr in chrs)
            {
                int idx = comboBoxCharacter.Items.Add(chr.name);
                if (chr.name == selectedChr)
                    comboBoxCharacter.SelectedIndex = idx;
            }
            comboBoxCharacter.EndUpdate();
        }
        # endregion


        public Form1()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            prg = new e4dnd2obsidianProg();
            prg.doLogin();

            updateCampaignMenu();
        }

        private void btn_selFile_Click(object sender, EventArgs e)
        {
            Stream myStream;
            //openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog1.Filter = "DnD4e files (*.dnd4e)|*.dnd4e|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = openFileDialog1.OpenFile()) != null)
                {
                    string flname = openFileDialog1.FileName;
                    
                    XElement chrData = XElement.Load(myStream);
                    myStream.Close();

                    prg.setFile(flname, chrData);
                }
            }
        }
        
        private void comboBoxCampaign_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selCmp = (string)comboBoxCampaign.SelectedItem;
            prg.setCampaign(selCmp);
            updateCharacterMenu();
        }

        private void comboBoxCharacter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selChr = (string)comboBoxCharacter.SelectedItem;
            prg.setCharacter(selChr);
        }

        private void btn_update_chr_Click(object sender, EventArgs e)
        {
            prg.updateCharacter();
        }

        private void button_create_chr_Click(object sender, EventArgs e)
        {
            prg.createCharacter();
        }

        

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_send_testdata_Click(object sender, EventArgs e)
        {
            string tst_data = txt_json_input.Text;
            try
            {
                prg.test(tst_data);
            }
            catch (Exception ex)
            {
                txt_msg_output.Text = ex.Message;
            }
        }
    }
}
