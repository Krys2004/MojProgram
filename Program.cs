using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class Urzadzenie {
    public string Nazwa { get; set; }
    public decimal CenaNetto { get; set; }
    public int Ilosc { get; set; }
    public decimal SumaNetto => CenaNetto * Ilosc;
    public decimal SumaBrutto => SumaNetto * 1.23m;
}

public class KosztorysApp : Form {
    private List<Urzadzenie> lista = new List<Urzadzenie>();
    private DataGridView grid;
    private TextBox txtNazwa, txtCena, txtIlosc;

    public KosztorysApp() {
        this.Text = "Pro Generator Kosztorysów v2.0";
        this.Size = new Size(600, 500);
        this.StartPosition = FormStartPosition.CenterScreen;

        // Panel wprowadzania
        Label lbl1 = new Label() { Text = "Urządzenie:", Left = 10, Top = 10, Width = 80 };
        txtNazwa = new TextBox() { Left = 100, Top = 10, Width = 150 };

        Label lbl2 = new Label() { Text = "Cena Netto:", Left = 260, Top = 10, Width = 80 };
        txtCena = new TextBox() { Left = 340, Top = 10, Width = 60 };

        Label lbl3 = new Label() { Text = "Ilość:", Left = 410, Top = 10, Width = 40 };
        txtIlosc = new TextBox() { Left = 460, Top = 10, Width = 30 };

        Button btnDodaj = new Button() { Text = "DODAJ", Left = 500, Top = 8, Width = 70 };
        btnDodaj.Click += (s, e) => DodajUrzadzenie();

        // Tabela
        grid = new DataGridView() { Left = 10, Top = 50, Width = 560, Height = 300, ReadOnly = true };
        grid.ColumnCount = 5;
        grid.Columns[0].Name = "Nazwa";
        grid.Columns[1].Name = "Cena Netto";
        grid.Columns[2].Name = "Ilość";
        grid.Columns[3].Name = "Suma Netto";
        grid.Columns[4].Name = "Suma Brutto (23%)";

        // Przycisk Zapisu
        Button btnExport = new Button() { 
            Text = "GENERUJ DOKUMENT PDF (HTML)", 
            Left = 10, Top = 370, Width = 560, Height = 50, 
            BackColor = Color.Gold, Font = new Font(this.Font, FontStyle.Bold) 
        };
        btnExport.Click += (s, e) => EksportujDoHTML();

        this.Controls.AddRange(new Control[] { lbl1, txtNazwa, lbl2, txtCena, lbl3, txtIlosc, btnDodaj, grid, btnExport });
    }

    private void DodajUrzadzenie() {
        try {
            var u = new Urzadzenie {
                Nazwa = txtNazwa.Text,
                CenaNetto = decimal.Parse(txtCena.Text),
                Ilosc = int.Parse(txtIlosc.Text)
            };
            lista.Add(u);
            grid.Rows.Add(u.Nazwa, u.CenaNetto, u.Ilosc, u.SumaNetto, u.SumaBrutto.ToString("F2"));
            txtNazwa.Clear(); txtCena.Clear(); txtIlosc.Clear();
        } catch { MessageBox.Show("Wpisz poprawne liczby!"); }
    }

    private void EksportujDoHTML() {
        if (lista.Count == 0) return;
        string pulpit = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string sciezka = Path.Combine(pulpit, "Kosztorys_Profesjonalny.html");

        decimal totalNetto = lista.Sum(x => x.SumaNetto);
        decimal totalBrutto = lista.Sum(x => x.SumaBrutto);

        string html = "<html><head><style>table{width:100%;border-collapse:collapse;} th,td{border:1px solid #ddd;padding:8px;text-align:left;} th{background-color:#f2f2f2;}</style></head><body>" +
                      "<h1>Kosztorys Urządzeń</h1><p>Data: " + DateTime.Now.ToShortDateString() + "</p><table>" +
                      "<tr><th>Nazwa</th><th>Cena Netto</th><th>Ilość</th><th>Suma Netto</th><th>Suma Brutto</th></tr>";

        foreach (var u in lista) {
            html += $"<tr><td>{u.Nazwa}</td><td>{u.CenaNetto}</td><td>{u.Ilosc}</td><td>{u.SumaNetto}</td><td>{u.SumaBrutto:F2}</td></tr>";
        }

        html += $"</table><h3>Suma Netto: {totalNetto:F2} PLN</h3>" +
                $"<h2>DO ZAPŁATY (BRUTTO): {totalBrutto:F2} PLN</h2>" +
                "<p><i>Aby zapisać jako PDF: Naciśnij Ctrl+P w przeglądarce i wybierz 'Zapisz jako PDF'.</i></p></body></html>";

        File.WriteAllText(sciezka, html);
        MessageBox.Show("Plik wygenerowany na pulpicie! Otwórz go i zapisz jako PDF.");
    }

    [STAThread] static void Main() { Application.EnableVisualStyles(); Application.Run(new KosztorysApp()); }
}
