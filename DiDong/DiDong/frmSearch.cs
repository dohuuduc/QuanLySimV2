using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyData {
  public partial class frmSearch : Form {
    public frmSearch() {
      InitializeComponent();
      BindController();
    }
    private Label[] lbl = null;
    private TextBox[] textBoxes = null;

    private void frmSearch_Load(object sender, EventArgs e) {

    }
    private void BindController() {
      List<dm_column> model = SQLDatabase.Loaddm_column("select * from dm_column order by orderid");
      this.lbl = new Label[model.Count()];
      this.textBoxes = new TextBox[model.Count()];

      int index = 0;
      foreach (dm_column item in model) {
        lbl[index] = CreateLable(index, item);

        textBoxes[index] = CreateTextbox(index, item.ma);

        panelTim.Controls.Add(lbl[index]);
        panelTim.Controls.Add(textBoxes[index]);
        index++;
      }
      textBoxes[0].Focus();
    }
    private Label CreateLable(int index, dm_column model) {
      Label lbl = new Label();
      lbl.Location = new Point(9, 20 + index * 35);
      lbl.Name = string.Format("lbl_", model.ma);
      lbl.Text = model.name;
      return lbl;
    }
    private TextBox CreateTextbox(int index, string name) {
      TextBox lbl = new TextBox();
      lbl.Location = new Point(120, 20 + index * 35);
      lbl.Name = string.Format("txt_", name);
      lbl.Size = new Size(228, 20);
      return lbl;
    }
  }
}
