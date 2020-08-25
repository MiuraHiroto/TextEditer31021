using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditer31021
{
    public partial class Form1 : Form
    {
        //現在編集中のファイル名
        private string fileName = ""; //Camel形式(⇔Pascal形式)
        public Form1()
        {
            InitializeComponent();
        }
        //終了
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //アプリケーション終了
            Application.Exit();
        }
        //新規作成
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileName = "";
            rtTextArea.Text = "";
        }
        //開く
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //[開く]ダイアログを表示
            if (ofdFileOpen.ShowDialog() == DialogResult.OK)
            {
                //StreamReaderクラスを使用してファイル読込み
                using (StreamReader sr = new StreamReader(ofdFileOpen.FileName, Encoding.GetEncoding("utf-8"), false))
                {
                    rtTextArea.Text = sr.ReadToEnd();
                }
            }
        }
        //上書き保存
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.fileName != "")
            {
                FileSave(fileName);
            }
            else
            {
                SaveNameToolStripMenuItem_Click(sender, e);
            }
        }
        //名前を付けて保存
        private void SaveNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //[名前を付けて保存]ダイアログを表示
            if (sfdFileSave.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sfdFileSave.FileName, false, Encoding.GetEncoding("utf-8")))
                {
                    sw.WriteLine(rtTextArea.Text);
                }
            }
        }
        //ファイル名を指定しデータを保存
        private void FileSave(string fileName)
        {
            using (StreamWriter sw = new StreamWriter(sfdFileSave.FileName, true, Encoding.GetEncoding("utf-8")))
            {
                sw.WriteLine(rtTextArea.Text);
            }
        }
 
        //元に戻す
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
                rtTextArea.Undo();
        }
        //やり直し
        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        { 
                rtTextArea.Redo();
        }
        //切り取り
        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtTextArea.SelectionLength > 0)
            {
                rtTextArea.Cut();
            }
        }
        //コピー
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtTextArea.SelectionLength > 0)
            {
                
                rtTextArea.Copy();
            }
      
        }
        //貼り付け
        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data != null && data.GetDataPresent(DataFormats.Text) == true)
            {
                rtTextArea.Paste();
            }
        }
        //削除
        private void DelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtTextArea.SelectionLength > 0)
            {
                rtTextArea.Text = "";
            }
        }
        //編集
        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*if (rtTextArea.CanUndo)
            {
                UndoToolStripMenuItem.Enabled = true;
            }
            else
            {
                UndoToolStripMenuItem.Enabled = false;
            }
            if (rtTextArea.CanRedo)
            {
                RedoToolStripMenuItem.Enabled = true;
            }
            else
            {
                RedoToolStripMenuItem.Enabled = false;
            }*/
        }
        //編集メニュー項目内のマスク処理
        private void EditMenuMaskCheck()
        {
            //DataFormats.Format myFomat = DataFormats.GetFormat(DataFormats.Rtf);
            UndoToolStripMenuItem.Enabled = rtTextArea.CanUndo ? true : false;
            RedoToolStripMenuItem.Enabled = rtTextArea.CanRedo ? true : false;
            CutToolStripMenuItem.Enabled = (rtTextArea.SelectionLength > 0);
            CopyToolStripMenuItem.Enabled = (rtTextArea.SelectionLength > 0);
            PasteToolStripMenuItem.Enabled = Clipboard.GetDataObject().GetDataPresent(DataFormats.Rtf);
        }
        //色
        private void ColorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (cdColor.ShowDialog() == DialogResult.OK)
            {
                rtTextArea.SelectionColor = cdColor.Color;
            }
        }
        //フォント
        private void FontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cdFont.ShowDialog() == DialogResult.OK)
            {
                rtTextArea.SelectionFont = cdFont.Font;
            }
        }
    }
}
