using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Numerics;

namespace Fale
{
    public partial class Form1 : Form
    {

        InitialVar ini = new InitialVar();
        private FormWindowState mLastState;

        public Form1()
        {
            InitializeComponent();
            zmienne_start();
            check_stosunek(false);
            uruchom(false);
            checkUpdateIni();
            mLastState = this.WindowState;
        }

        public void checkUpdateIni()
        {
            Ping pg = new Ping();
            bool status;
            try
            {
                PingReply reply = pg.Send("216.58.209.67");
                status = true;
            }
            catch
            {
                status = false;
            }
            if (status)
            {

                try
                {
                    WebClient myWebClient = new WebClient();
                    myWebClient.DownloadFile("http://piaskowy.ayz.pl/fale/version.txt", "tmp/version.txt");
                }
                catch{}
                if (File.Exists("tmp/version.txt"))
                {
                    string[] tab1 = File.ReadAllLines("tmp/version.txt");
                    int server = int.Parse(tab1[0]);
                    if (File.Exists("src/version.txt"))
                    {
                        string[] tab2 = File.ReadAllLines("src/version.txt");
                        int user = int.Parse(tab2[0]);
                        if (server > user)
                        {
                            var notify = new NotifyIcon();
                            notify.Visible = true;
                            notify.Icon = System.Drawing.SystemIcons.Information;
                            notify.ShowBalloonTip(10000, "Aktualizacja", "Dostępna jest nowa wersja programu 'fale', naciśnij aby zainstalować.", ToolTipIcon.Info);
                            notify.BalloonTipClicked += new EventHandler(notify_BallonTipClicked);
                        }
                    }
                }

            }
        }

        void notify_BallonTipClicked(object obj, EventArgs e)
        {
            Process update = new Process();
            update.StartInfo.FileName = "update.exe";
            update.Start();
            Application.Exit();
        }

        public void button_change(int n, TextBox o, int max, int min)
        {
            ini.tmp_s = o.Text;
            if (max > 0)
            {
                if (ini.tmp_s != "")
                {
                    if (check_num_i(ini.tmp_s))
                    {
                        ini.tmp2 = int.Parse(ini.tmp_s);
                        if (ini.tmp2 < min)
                        {
                            ini.tmp2 = min;
                            o.Text = ini.tmp2.ToString();
                        }
                        else if (ini.tmp2 > max)
                        {
                            ini.tmp2 = max;
                            o.Text = ini.tmp2.ToString();
                        }
                        else
                        {
                            if (ini.tmp2 > min && ini.tmp2 < max)
                            {
                                ini.tmp2 += n;
                                o.Text = ini.tmp2.ToString();
                            }
                            if ((ini.tmp2 == min && n > 0) || (ini.tmp2 == max && n < 0))
                            {
                                ini.tmp2 += n;
                                o.Text = ini.tmp2.ToString();
                            }
                        }
                    }
                    else
                    {
                        o.Text = min.ToString();
                    }
                }
                else
                {
                    ini.tmp2 = 0;
                    if (n > 0)
                    {
                        ini.tmp2 += n;
                    }
                    else
                    {
                        ini.tmp2 = min;
                    }
                    o.Text = ini.tmp2.ToString();
                }
            }
            else
            {
                if (ini.tmp_s != "")
                {
                    if (check_num_i(ini.tmp_s))
                    {
                        ini.tmp2 = int.Parse(ini.tmp_s);
                        if (ini.tmp2 > min)
                        {
                            ini.tmp2 = min;
                            o.Text = ini.tmp2.ToString();
                        }
                        else if (ini.tmp2 < max)
                        {
                            ini.tmp2 = max;
                            o.Text = ini.tmp2.ToString();
                        }
                        else
                        {
                            if (ini.tmp2 < min && ini.tmp2 > max)
                            {
                                ini.tmp2 += n;
                                o.Text = ini.tmp2.ToString();
                            }
                            if ((ini.tmp2 == min && n < 0) || (ini.tmp2 == max && n > 0))
                            {
                                ini.tmp2 += n;
                                o.Text = ini.tmp2.ToString();
                            }
                        }
                    }
                    else
                    {
                        o.Text = min.ToString();
                    }
                }
                else
                {
                    ini.tmp2 = 0;
                    if (n < 0)
                    {
                        ini.tmp2 += n;
                    }
                    else
                    {
                        ini.tmp2 = min;
                    }
                    o.Text = ini.tmp2.ToString();
                }
            }
            wczytaj_var();
        }

        public void button_change_min(double n, TextBox o, double max, double min)
        {
            ini.tmp_s = o.Text;
            if (max > 0)
            {
                if (ini.tmp_s != "")
                {
                    if (check_num(ini.tmp_s))
                    {
                        ini.tmp2_d = double.Parse(ini.tmp_s);
                        if (ini.tmp2_d < min)
                        {
                            ini.tmp2_d = min;
                            o.Text = ini.tmp2_d.ToString();
                        }
                        else if (ini.tmp2_d > max)
                        {
                            ini.tmp2_d = max;
                            o.Text = ini.tmp2_d.ToString();
                        }
                        else
                        {
                            if (ini.tmp2_d > min && ini.tmp2_d < max)
                            {
                                ini.tmp2_d += n;
                                o.Text = ini.tmp2_d.ToString();
                            }
                            if ((ini.tmp2_d == min && n > 0) || (ini.tmp2_d == max && n < 0))
                            {
                                ini.tmp2_d += n;
                                o.Text = ini.tmp2_d.ToString();
                            }
                        }
                    }
                    else
                    {
                        o.Text = min.ToString();
                    }
                }
                else
                {
                    ini.tmp2_d = 0;
                    ini.tmp2_d += n;
                    o.Text = ini.tmp2_d.ToString();
                }
            }
            else
            {
                if (ini.tmp_s != "")
                {
                    if (check_num(ini.tmp_s))
                    {
                        ini.tmp2_d = double.Parse(ini.tmp_s);
                        if (ini.tmp2_d < min)
                        {
                            ini.tmp2_d = min;
                            o.Text = ini.tmp2_d.ToString();
                        }
                        else if (ini.tmp2_d > max)
                        {
                            ini.tmp2_d = max;
                            o.Text = ini.tmp2_d.ToString();
                        }
                        else
                        {
                            if (ini.tmp2_d < min && ini.tmp2_d > max)
                            {
                                ini.tmp2_d += n;
                                o.Text = ini.tmp2_d.ToString();
                            }
                            if ((ini.tmp2_d == min && n < 0) || (ini.tmp2_d == max && n > 0))
                            {
                                ini.tmp2_d += n;
                                o.Text = ini.tmp2.ToString();
                            }
                        }
                    }
                    else
                    {
                        o.Text = min.ToString();
                    }
                }
                else
                {
                    ini.tmp2_d = 0;
                    ini.tmp2_d += n;
                    o.Text = ini.tmp2_d.ToString();
                }
            }
            wczytaj_var();
        }

        public bool check_num(string x, string info = "")
        {
            char[] cyfry = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            int len;
            bool check = false;
            if (x.ToUpper() == x.ToLower())
            {
                if (x.IndexOf('.') != -1)
                {
                    ini.ok = false;
                    MessageBox.Show(info, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                else
                {
                    if (x.IndexOf(',') != -1)
                    {
                        x = x.Replace(',', '.');
                    }
                    len = x.Length;
                    char[] tab = x.ToCharArray();
                    for (int i = 0; i < len; i++)
                    {
                        check = false;
                        for (int k = 0; k < 10; k++)
                        {
                            if (tab[i] == cyfry[k])
                            {
                                check = true;
                                break;
                            }
                        }
                    }
                    if (check == true)
                    {
                        return true;
                    }
                    else
                    {
                        ini.ok = false;
                        MessageBox.Show(info, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                }
            }
            else
            {
                ini.ok = false;
                MessageBox.Show(info, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        public bool check_num_i(string x, string info = "")
        {
            char[] cyfry = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            int len;
            bool check = false;
            if (x.ToUpper() == x.ToLower())
            {
                if (x.IndexOf('.') != -1 || x.IndexOf(',') != -1)
                {
                    ini.ok = false;
                    MessageBox.Show(info, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                else
                {
                    len = x.Length;
                    char[] tab = x.ToCharArray();
                    for (int i = 0; i < len; i++)
                    {
                        check = false;
                        for (int k = 0; k < 10; k++)
                        {
                            if (tab[i] == cyfry[k])
                            {
                                check = true;
                                break;
                            }
                        }
                    }
                    if (check == true)
                    {
                        return true;
                    }
                    else
                    {
                        ini.ok = false;
                        MessageBox.Show(info, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                }
            }
            else
            {
                ini.ok = false;
                MessageBox.Show(info, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        public int comparmentI(int var, int min, int max, TextBox obj)
        {
            var = int.Parse(obj.Text);
            if (var < min)
            {
                obj.Text = min.ToString();
                return min;
            }
            else if (var > max)
            {
                obj.Text = max.ToString();
                return max;
            }
            else
            {
                return var;
            }
        }

        public int comparmentI2(int var, int min, int max, TrackBar obj)
        {
            var = obj.Value;
            if (var < min)
            {
                obj.Text = min.ToString();
                obj.Value = min;
                return min;
            }
            else if (var > max)
            {
                obj.Text = max.ToString();
                obj.Value = max;
                return max;
            }
            else
            {
                obj.Value = var;
                return var;
            }
        }

        public double comparmentD(double var, double min, double max, TextBox obj)
        {
            var = double.Parse(obj.Text);
            if (var < min)
            {
                obj.Text = min.ToString();
                return min;
            }
            else if (var > max)
            {
                obj.Text = max.ToString();
                return max;
            }
            else
            {
                return var;
            }
        }

        public void checkerHelper2(CheckBox check, GroupBox gr1, GroupBox gr2)
        {
            if (check.Checked == true)
            {
                gr1.Enabled = false;
                gr2.Enabled = true;
            }
            else
            {
                gr1.Enabled = true;
                gr2.Enabled = false;
            }
        }

        public void checkerHelper1(CheckBox check, GroupBox gr)
        {
            if (check.Checked == true)
            {
                gr.Enabled = false;
            }
            else
            {
                gr.Enabled = true;
            }
        }

        public void wczytaj_var()
        {
            ini.t = 0;
            ini.status = false;
            ini.ok = true;

            if (check_num_i(box_max_y.Text, "Sprawdź poprawność danych w sekcji 'Skala osi Y'.") == true)
            {
                ini.var_max_y = comparmentI(ini.var_max_y, ini.max_y, ini.m_max_y, box_max_y);
            }

            if (check_num_i(box_min_y.Text, "Sprawdź poprawność danych w sekcji 'Skala osi Y'.") == true)
            {
                ini.var_min_y = comparmentI(ini.var_min_y, ini.m_min_y, ini.min_y, box_min_y);
            }

            if (check_num_i(box_max_x.Text, "Sprawdź poprawność danych w sekcji 'Skala osi X'.") == true)
            {
                ini.var_max_x = comparmentI(ini.var_max_x, ini.max_x, ini.m_max_x, box_max_x);
            }

            if (check_num_i(box_velocity_show.Text, "Sprawdź poprawność danych w sekcji 'Szybkość wyświetlania'.") == true)
            {
                ini.var_szybkosc_wyswietlania = comparmentI(ini.var_szybkosc_wyswietlania, ini.szybkosc_wyswietlania, ini.m_szybkosc_wyswietlania, box_velocity_show);
            }

            if (check_num(box_stosunek_v.Text, "Sprawdź poprawność danych w sekcji 'Szybkość fali'.") == true)
            {
                ini.var_stosunek_v_ = comparmentD(ini.var_stosunek_v_, ini.stosunek_v_, ini.m_stosunek_v_, box_stosunek_v);
            }

            if (check_num_i(box_bazowe_v.Text, "Sprawdź poprawność danych w sekcji 'Szybkość fali''.") == true)
            {
                ini.var_bazowa_v = comparmentI(ini.var_bazowa_v, ini.bazowa_v, ini.m_bazowa_v, box_bazowe_v);
            }

            if (check_num(box_v_1.Text, "Sprawdź poprawność danych w sekcji 'Szybkość pierwszej fali''.") == true)
            {
                ini.var_v_1 = comparmentD(ini.var_v_1, ini.minimum_v_1, ini.m_v_1, box_v_1);
            }

            if (check_num(box_v_2.Text, "Sprawdź poprawność danych w sekcji 'Szybkość drugiej fali''.") == true)
            {
                ini.var_v_2 = comparmentD(ini.var_v_2, ini.minimum_v_2, ini.m_v_2, box_v_2);
            }

            if (check_num(box_a_stosunek.Text, "Sprawdź poprawność danych w sekcji 'Amplituda fali'.") == true)
            {
                ini.var_stosunek_a_ = comparmentD(ini.var_stosunek_a_, ini.stosunek_a_, ini.m_stosunek_a_, box_a_stosunek);
            }

            if (check_num(box_a_bazowe.Text, "Sprawdź poprawność danych w sekcji 'Amplituda fali'.") == true)
            {
                ini.var_bazowa_a = comparmentI(ini.var_bazowa_a, ini.bazowa_a, ini.m_bazowa_a, box_a_bazowe);
            }

            if (check_num(box_a_1.Text, "Sprawdź poprawność danych w sekcji 'Amplituda pierwszej fali'.") == true)
            {
                ini.var_a_1 = comparmentD(ini.var_a_1, ini.a_1, ini.m_a_1, box_a_1);
            }

            if (check_num(box_a_2.Text, "Sprawdź poprawność danych w sekcji 'Amplituda drugiej fali'.") == true)
            {
                ini.var_a_2 = comparmentD(ini.var_a_2, ini.a_2, ini.m_a_2, box_a_2);
            }

            if (check_num(box_stosunek_f.Text, "Sprawdź poprawność danych w sekcji 'Częstotliwość fali'.") == true)
            {
                ini.var_stosunek_f_ = comparmentD(ini.var_stosunek_f_, ini.stosunek_f_, ini.m_stosunek_f_, box_stosunek_f);
            }

            if (check_num(box_bazowe_f.Text, "Sprawdź poprawność danych w sekcji 'Częstotliwość fali'.") == true)
            {
                ini.var_bazowa_f = comparmentD(ini.var_bazowa_f, ini.bazowa_f, ini.m_bazowa_f, box_bazowe_f);
            }

            if (check_num_i(box_f_1.Text, "Sprawdź poprawność danych w sekcji 'Częstotliwość pierwszej fali'.") == true)
            {
                ini.var_f_1 = comparmentI(ini.var_f_1, ini.f_1, ini.m_f_1, box_f_1);
            }

            if (check_num_i(box_f_2.Text, "Sprawdź poprawność danych w sekcji 'Częstotliwość drugiej fali'.") == true)
            {
                ini.var_f_2 = comparmentI(ini.var_f_2, ini.f_2, ini.m_f_2, box_f_2);
            }

            if (check_num(box_faza_1.Text, "Sprawdź poprawność danych w sekcji 'Faza pierwszej fali'.") == true)
            {
                ini.var_faza_1 = comparmentD(ini.var_faza_1, ini.faza_1, ini.m_faza_1, box_faza_1);
                track_r_f1.Value = int.Parse(ini.var_faza_1.ToString());
                ini.var_faza_1 = ini.var_faza_1 * 3.14159 / 180;
            }

            if (check_num(box_faza_2.Text, "Sprawdź poprawność danych w sekcji 'Faza drugiej fali'.") == true)
            {
                ini.var_faza_2 = comparmentD(ini.var_faza_2, ini.faza_2, ini.m_faza_2, box_faza_2);
                track_r_f2.Value = int.Parse(ini.var_faza_2.ToString());
                ini.var_faza_2 = ini.var_faza_2 * 3.14159 / 180;
            }

            if (check_num_i(box_pion.Text, "Sprawdź poprawność danych w sekcji 'Wymiary wykresu'.") == true)
            {
                ini.var_wymiar_y = comparmentI(ini.var_wymiar_y, ini.wymiar_y, ini.m_wymiar_y, box_pion);
            }

            if (check_num_i(box_poziom.Text, "Sprawdź poprawność danych w sekcji 'Wymiary wykresu'.") == true)
            {
                ini.var_wymiar_x = comparmentI(ini.var_wymiar_x, ini.wymiar_x, ini.m_wymiar_x, box_poziom);
            }

            if (check_num(box_krok.Text, "Sprawdź poprawność danych w sekcji 'Zaawansowane'.") == true)
            {
                ini.var_krok = comparmentI(ini.var_krok, ini.krok, ini.m_krok, box_krok);
            }

            if (check_num(box_krok_t.Text, "Sprawdź poprawność danych w sekcji 'Zaawansowane'.") == true)
            {
                ini.var_krok_t = comparmentI(ini.var_krok_t, ini.krok_t, ini.m_krok_t, box_krok_t);
            }

            if (check_num_i(box_pkt.Text, "Sprawdź poprawność danych w sekcji 'Współrzędna x punktu'.") == true)
            {
                ini.m_pkt = ini.var_max_x / ini.var_krok;
                ini.var_pkt = comparmentI(ini.var_pkt, ini.pkt, ini.m_pkt, box_pkt);
            }

            if ((string)box_styl_1.SelectedItem == "Liniowy" || (string)box_styl_1.SelectedItem == "Punktowy")
            {
                ini.var_styl_1 = box_styl_1.SelectedItem.ToString();
            }
            else
            {
                box_styl_1.SelectedItem = ini.var_styl_1;
            }

            if ((string)box_styl_2.SelectedItem == "Liniowy" || (string)box_styl_2.SelectedItem == "Punktowy")
            {
                ini.var_styl_2 = box_styl_2.SelectedItem.ToString();
            }
            else
            {
                box_styl_2.SelectedItem = ini.var_styl_2;
            }

            if ((string)box_styl_3.SelectedItem == "Liniowy" || (string)box_styl_3.SelectedItem == "Punktowy")
            {
                ini.var_styl_3 = box_styl_3.SelectedItem.ToString();
            }
            else
            {
                box_styl_3.SelectedItem = ini.var_styl_3;
            }

            ini.w_f_1 = check_fala_1.Checked;
            ini.w_f_2 = check_fala_2.Checked;
            ini.w_f_w = check_fala_wypadkowa.Checked;
            ini.s_v = check_stosunek_v.Checked;
            ini.s_a = check_stosunek_a.Checked;
            ini.s_f = check_stosunek_f.Checked;
            ini.c_pkt = check_pkt.Checked;

            if (check_stosunek_a.Checked == true)
            {
                t_a1.Text = ini.var_bazowa_a.ToString();
                ini.tmp = ini.var_bazowa_a * ini.var_stosunek_a_;
                if (radio_pkt_f1.Checked == true)
                {
                    wynik_a.Text = ini.var_bazowa_a.ToString();
                }
                else if (radio_pkt_f2.Checked == true)
                {
                    wynik_a.Text = Math.Round(ini.tmp, 2).ToString();
                }
                else
                {
                    wynik_a.Text = "-";
                }
            }
            else
            {
                t_a1.Text = ini.var_a_1.ToString();
                t_a2.Text = ini.var_a_2.ToString();
                wynik_a.Text = Math.Round(ini.tmp, 1).ToString();
                if (radio_pkt_f1.Checked == true)
                {
                    wynik_a.Text = Math.Round(ini.var_a_1, 2).ToString();
                }
                else if (radio_pkt_f2.Checked == true)
                {
                    wynik_a.Text = Math.Round(ini.var_a_2, 2).ToString();
                }
                else
                {
                    wynik_a.Text = "-";
                }
            }

            if (check_stosunek_f.Checked == true)
            {
                t_f1.Text = ini.var_bazowa_f.ToString();
                ini.tmp = ini.var_bazowa_f * ini.var_stosunek_f_;
                t_f2.Text = ini.tmp.ToString();
                if (radio_pkt_f1.Checked == true)
                {
                    wynik_f.Text = Math.Round(ini.var_bazowa_f, 2).ToString();
                }
                else if (radio_pkt_f2.Checked == true)
                {
                    wynik_f.Text = Math.Round(ini.tmp, 2).ToString();
                }
                else
                {
                    wynik_f.Text = "-";
                }
            }
            else
            {
                t_f1.Text = ini.var_f_1.ToString();
                t_f2.Text = ini.var_f_2.ToString();
                if (radio_pkt_f1.Checked == true)
                {
                    wynik_f.Text = ini.var_f_1.ToString();
                }
                else if (radio_pkt_f2.Checked == true)
                {
                    wynik_f.Text = ini.var_f_2.ToString();
                }
                else
                {
                    wynik_f.Text = "-";
                }
            }

            if (check_stosunek_v.Checked == true)
            {
                t_v1.Text = ini.var_bazowa_v.ToString();
                ini.tmp = ini.var_bazowa_v * ini.var_stosunek_v_;
                t_v2.Text = ini.tmp.ToString();
                if (radio_pkt_f1.Checked == true)
                {
                    wynik_v.Text = ini.var_bazowa_v.ToString();
                }
                else if (radio_pkt_f2.Checked == true)
                {
                    wynik_v.Text = Math.Round(ini.tmp, 2).ToString();
                }
                else
                {
                    wynik_v.Text = "  -";
                }
            }
            else
            {
                t_v1.Text = ini.var_v_1.ToString();
                t_v2.Text = ini.var_v_2.ToString();
                if (radio_pkt_f1.Checked == true)
                {
                    wynik_v.Text = Math.Round(ini.var_v_1, 2).ToString();
                }
                else if (radio_pkt_f2.Checked == true)
                {
                    wynik_v.Text = Math.Round(ini.var_v_2, 2).ToString();
                }
                else
                {
                    wynik_v.Text = "  -";
                }
            }

            if (radio_pkt_wypadkowa.Checked == false)
            {
                int wynik_x1 = 0;
                int wynik_x2 = 0;
                wynik_x1 = wynik_a.Size.Width + 30;
                wynik2.Location = new System.Drawing.Point(wynik_x1, 49);
                wynik_x1 += 37;
                wynik_f.Location = new System.Drawing.Point(wynik_x1, 49);
                wynik_x1 += wynik_f.Size.Width - 3;
                wynik3.Location = new System.Drawing.Point(wynik_x1, 49);
                wynik_x2 = wynik_v.Size.Width;
                wynik_x1 += 41 - wynik_x2 / 2;
                wynik_v.Location = new System.Drawing.Point(wynik_x1, 58);
                wynik_x1 += wynik_x2 / 4;
                wynik6.Location = new System.Drawing.Point(wynik_x1, 41);
                wynik_x1 += 25;
                wynik5.Location = new System.Drawing.Point(wynik_x1, 49);
            }

            //////////////////////////////////////////////////////
            if (check_stosunek_v.Checked == false)
            {
                if (check_left1.Checked == true)
                {
                    if (ini.var_v_1 > 0)
                    {
                        ini.var_v_1 = ini.var_v_1 * (-1);
                    }
                }
                else
                {
                    if (ini.var_v_1 < 0)
                    {
                        ini.var_v_1 = ini.var_v_1 * (-1);
                    }
                }

                if (check_left2.Checked == true)
                {
                    if (ini.var_v_2 > 0)
                    {
                        ini.var_v_2 = ini.var_v_2 * (-1);
                    }
                }
                else
                {
                    if (ini.var_v_2 < 0)
                    {
                        ini.var_v_2 = ini.var_v_2 * (-1);
                    }
                }
            }

            ini.tmp = ini.var_faza_1 * 180 / 3.14159;
            t_fa1.Text = ini.tmp.ToString();

            ini.tmp = (ini.var_faza_2 + ini.var_r_faz) * 180 / 3.14159;
            t_fa2.Text = ini.tmp.ToString();

            if (check_num(box_s_f1.Text, "Sprawdź poprawność danych w sekcji 'Częstotliwość dźwięku'.") == true)
            {
                ini.var_s_f1 = comparmentI(ini.var_s_f1, ini.s_f1, ini.m_s_f1, box_s_f1);
                track_s_f1.Value = ini.var_s_f1;
            }

            if (check_num(box_s_a1.Text, "Sprawdź poprawność danych w sekcji 'Amplituda dźwięku'.") == true)
            {
                ini.var_s_a1 = comparmentI(ini.var_s_a1, ini.s_a1, ini.m_s_a1, box_s_a1);
            }

            if (check_num(box_s_f2.Text, "Sprawdź poprawność danych w sekcji 'Częstotliwość dźwięku'.") == true)
            {
                ini.var_s_f2 = comparmentI(ini.var_s_f2, ini.s_f2, ini.m_s_f2, box_s_f2);
                track_s_f2.Value = ini.var_s_f2;
            }

            if (check_num(box_s_a2.Text, "Sprawdź poprawność danych w sekcji 'Amplituda dźwięku'.") == true)
            {
                ini.var_s_a2 = comparmentI(ini.var_s_a2, ini.s_a2, ini.m_s_a2, box_s_a2);
                track_s_a2.Value = ini.var_s_a2;
            }            
            wykres();
        }

        public void zmienne_start()
        {
            check_fala_1.Checked = ini.ini_w_f_1;
            check_fala_2.Checked = ini.ini_w_f_2;
            check_fala_wypadkowa.Checked = ini.ini_w_f_w;
            check_stosunek_v.Checked = ini.ini_s_v;
            check_stosunek_a.Checked = ini.ini_s_a;
            check_stosunek_f.Checked = ini.ini_s_f;
            check_pkt.Checked = ini.ini_c_pkt;
            radio_m.Checked = ini.ini_r_m;
            radio_s.Checked = ini.ini_r_s;
            radio_d.Checked = ini.ini_r_d;
            box_velocity_show.Text = ini.ini_var_szybkosc_wyswietlania.ToString();
            box_stosunek_v.Text = ini.ini_var_stosunek_v_.ToString();
            box_bazowe_v.Text = ini.ini_var_bazowa_v.ToString();
            box_v_1.Text = ini.ini_var_v_1.ToString();
            box_v_2.Text = ini.ini_var_v_2.ToString();
            box_a_stosunek.Text = ini.ini_var_stosunek_a_.ToString();
            box_a_bazowe.Text = ini.ini_var_bazowa_a.ToString();
            box_a_1.Text = ini.ini_var_a_1.ToString();
            box_a_2.Text = ini.ini_var_a_2.ToString();
            box_stosunek_f.Text = ini.ini_var_stosunek_f_.ToString();
            box_bazowe_f.Text = ini.ini_var_bazowa_f.ToString();
            box_f_1.Text = ini.ini_var_f_1.ToString();
            box_f_2.Text = ini.ini_var_f_2.ToString();
            box_faza_1.Text = ini.ini_var_faza_1.ToString();
            box_faza_2.Text = ini.ini_var_faza_2.ToString();
            box_pkt.Text = ini.ini_var_pkt.ToString();
            box_s_f1.Text = ini.ini_var_s_f1.ToString();
            track_s_f1.Value = ini.ini_var_s_f1;
            box_s_a1.Text = ini.ini_var_s_a1.ToString();
            track_s_a1.Value = ini.ini_var_s_a1;
            box_s_f2.Text = ini.ini_var_s_f2.ToString();
            track_s_f2.Value = ini.ini_var_s_f2;
            box_s_a2.Text = ini.ini_var_s_a2.ToString();
            track_s_a2.Value = ini.ini_var_s_a2;

            box_max_y.Text = ini.ini_var_max_y.ToString();
            box_min_y.Text = ini.ini_var_min_y.ToString();
            box_max_x.Text = ini.ini_var_max_x.ToString();
            box_poziom.Text = ini.ini_var_wymiar_x.ToString();
            box_pion.Text = ini.ini_var_wymiar_y.ToString();
            box_krok.Text = ini.ini_var_krok.ToString();
            box_krok_t.Text = ini.ini_var_krok_t.ToString();
            check_zaawansowane.Checked = ini.ini_z_c;

            check_stereo.Checked = ini.ini_s_ch_stereo;
            check_s_w2.Checked = ini.ini_s_w2;

            box_styl_1.SelectedItem = ini.ini_var_styl_1;
            box_styl_2.SelectedItem = ini.ini_var_styl_2;
            box_styl_3.SelectedItem = ini.ini_var_styl_3;

            track_r_f1.Value = int.Parse(ini.ini_var_faza_1.ToString());
            track_r_f2.Value = int.Parse(ini.ini_var_faza_2.ToString());

            radio_show_w.Checked = ini.ini_r_s_w;
            radio_show_s.Checked = ini.ini_r_s_s;
            radio_show_sz.Checked = ini.ini_r_s_sz;

            check_left1.Checked = ini.ini_c_left1;
            check_right1.Checked = ini.ini_c_right1;
            check_left2.Checked = ini.ini_c_left2;
            check_right2.Checked = ini.ini_c_right2;

            radio_pkt_f1.Checked = ini.ini_s_pkt_f1;
            radio_pkt_f2.Checked = ini.ini_s_pkt_f2;
            radio_pkt_wypadkowa.Checked = ini.ini_s_pkt_wypadkowa;

            timer1.Stop();
            timer2.Stop();
        }

        public void check_stosunek(bool agr = true)
        {
            checkerHelper2(check_stosunek_v, group_v_fal, group_stosunek_v);
            checkerHelper2(check_stosunek_a, group_a_fal, group_stosunek_a);
            checkerHelper2(check_stosunek_f, group_f_fal, group_stosunek_f);

            checkerHelper1(check_zaawansowane, group_zaawansowane);
            checkerHelper1(check_pkt, groupBox6);

            wczytaj_var();
            if (agr)
            {
                uruchom(true);
            }
        }

        public void uruchom(bool x)
        {
            if (x == true)
            {
                stop_start.Text = "STOP";
                if (ini.s_v == true || ini.s_a == true || ini.s_f == true)
                {
                    timer2.Start();
                    timer1.Stop();
                }
                else
                {
                    timer1.Start();
                    timer2.Stop();
                }
            }
            else
            {
                timer1.Stop();
                timer2.Stop();
                stop_start.Text = "START";
            }
        }

        public void rysuj()
        {
            ini.active_pkt = 0;
            double[] tab_w1 = new double[ini.var_max_x + ini.var_krok + 1];
            double[] tab_w2 = new double[ini.var_max_x + ini.var_krok + 1];
            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            chart1.Series["Series5"].Points.Clear();
            if (ini.var_v_1 > 0 && ini.var_v_2 > 0)
            {
                if (check_pkt.Checked == false)
                {
                    for (int x = 0; x < ini.var_max_x + ini.var_krok; x += ini.var_krok)
                    {
                        ini.tmp1 = Math.Sin(3.14 * ini.var_f_1 / 10 * (ini.t - x) / ini.var_v_1 + ini.var_faza_1) * ini.var_a_1;
                        if (ini.w_f_1 == true)
                        {
                            chart1.Series["Series1"].Points.AddXY(x, ini.tmp1);
                        }

                        ini.tmp2 = Math.Sin(3.14 * ini.var_f_2 / 10 * (ini.t - x) / ini.var_v_2 + ini.var_faza_2) * ini.var_a_2;
                        if (ini.w_f_2 == true)
                        {
                            chart1.Series["Series2"].Points.AddXY(x, ini.tmp2);
                        }

                        if (ini.w_f_w == true)
                        {
                            chart1.Series["Series3"].Points.AddXY(x, ini.tmp1 + ini.tmp2);
                        }
                    }
                }
                else
                {
                    for (int x = 0; x < ini.var_max_x + ini.var_krok; x += ini.var_krok)
                    {
                        ini.active_pkt++;
                        ini.tmp1 = Math.Sin(3.14 * ini.var_f_1 / 10 * (ini.t - x) / ini.var_v_1 + ini.var_faza_1) * ini.var_a_1;
                        if (ini.w_f_1 == true)
                        {
                            chart1.Series["Series1"].Points.AddXY(x, ini.tmp1);
                            if (radio_pkt_f1.Checked == true)
                            {
                                if (ini.active_pkt == ini.var_pkt)
                                {
                                    chart1.Series["Series5"].Points.AddXY(0, ini.tmp1);
                                    chart1.Series["Series5"].Points.AddXY(x, ini.tmp1);
                                    wynik.Text = Math.Round(ini.tmp1, 3).ToString();
                                }
                            }
                        }

                        ini.tmp2 = Math.Sin(3.14 * ini.var_f_2 / 10 * (ini.t - x) / ini.var_v_2 + ini.var_faza_2) * ini.var_a_2;
                        if (ini.w_f_2 == true)
                        {
                            chart1.Series["Series2"].Points.AddXY(x, ini.tmp2);
                            if (radio_pkt_f2.Checked == true)
                            {
                                if (ini.active_pkt == ini.var_pkt)
                                {
                                    chart1.Series["Series5"].Points.AddXY(0, ini.tmp2);
                                    chart1.Series["Series5"].Points.AddXY(x, ini.tmp2);
                                    wynik.Text = Math.Round(ini.tmp2, 3).ToString();
                                }
                            }
                        }

                        if (ini.w_f_w == true)
                        {
                            chart1.Series["Series3"].Points.AddXY(x, ini.tmp1 + ini.tmp2);
                            if (radio_pkt_wypadkowa.Checked == true)
                            {
                                if (ini.active_pkt == ini.var_pkt)
                                {
                                    ini.tmp = ini.tmp1 + ini.tmp2;
                                    chart1.Series["Series5"].Points.AddXY(0, ini.tmp);
                                    chart1.Series["Series5"].Points.AddXY(x, ini.tmp);
                                    wynik.Text = Math.Round(ini.tmp, 3).ToString();
                                }
                            }
                        }
                    }
                }
                ini.t += ini.var_krok_t;
            }
            else
            {
                if (check_pkt.Checked == false)
                {
                    if (ini.var_v_1 < 0)
                    {
                        ini.v1 = ini.var_v_1 * (-1);
                        ini.i = 0;
                        for (int x = 0; x < ini.var_max_x + ini.var_krok; x += ini.var_krok)
                        {
                            tab_w1[ini.i] = Math.Sin(3.14 * ini.var_f_1 / 10 * (-ini.t - x) / ini.var_v_1 + ini.var_faza_1) * ini.var_a_1;
                            if (ini.w_f_1 == true)
                            {
                                chart1.Series["Series1"].Points.AddXY(x, tab_w1[ini.i]);
                            }
                            ini.i++;
                        }
                    }
                    else if (ini.var_v_1 == 0)
                    {
                        ini.i = 0;
                        for (int x = 0; x < ini.var_max_x + ini.var_krok; x += ini.var_krok)
                        {
                            tab_w1[ini.i] = 0;
                            ini.i++;
                        }
                    }
                    else if (ini.var_v_1 > 0)
                    {
                        ini.i = 0;
                        for (int x = 0; x < ini.var_max_x + ini.var_krok; x += ini.var_krok)
                        {
                            tab_w1[ini.i] = Math.Sin(3.14 * ini.var_f_1 / 10 * (ini.t - x) / ini.var_v_1 + ini.var_faza_1) * ini.var_a_1;
                            if (ini.w_f_1 == true)
                            {
                                chart1.Series["Series1"].Points.AddXY(x, tab_w1[ini.i]);
                            }
                            ini.i++;
                        }
                    }

                    if (ini.var_v_2 < 0)
                    {
                        ini.v2 = ini.var_v_2 * (-1);
                        ini.i = 0;
                        for (int x = 0; x < ini.var_max_x + ini.var_krok; x += ini.var_krok)
                        {
                            tab_w2[ini.i] = Math.Sin(3.14 * ini.var_f_2 / 10 * (-ini.t - x) / ini.var_v_2 + ini.var_faza_2) * ini.var_a_2;
                            if (ini.w_f_2 == true)
                            {
                                chart1.Series["Series2"].Points.AddXY(x, tab_w2[ini.i]);
                            }
                            ini.i++;
                        }
                    }
                    else if (ini.var_v_2 == 0)
                    {
                        ini.i = 0;
                        for (int x = 0; x < ini.var_max_x + ini.var_krok; x += ini.var_krok)
                        {
                            tab_w2[ini.i] = 0;
                            ini.i++;
                        }
                    }
                    else if (ini.var_v_2 > 0)
                    {
                        ini.i = 0;
                        for (int x = 0; x < ini.var_max_x + ini.var_krok; x += ini.var_krok)
                        {
                            tab_w2[ini.i] = Math.Sin(3.14 * ini.var_f_2 / 10 * (ini.t - x) / ini.var_v_2 + ini.var_faza_2) * ini.var_a_2;
                            if (ini.w_f_2 == true)
                            {
                                chart1.Series["Series2"].Points.AddXY(x, tab_w2[ini.i]);
                            }
                            ini.i++;
                        }
                    }

                    if (ini.w_f_w == true)
                    {
                        ini.i = 0;
                        for (int x = 0; x < ini.var_max_x + ini.var_krok; x += ini.var_krok)
                        {
                            chart1.Series["Series3"].Points.AddXY(x, tab_w1[ini.i] + tab_w2[ini.i]);
                            ini.i++;
                        }
                    }
                }
                else
                {
                    if (ini.var_v_1 < 0)
                    {
                        ini.v1 = ini.var_v_1 * (-1);
                        ini.i = 0;
                        for (int x = 0; x < ini.var_max_x + ini.var_krok; x += ini.var_krok)
                        {
                            ini.active_pkt++;
                            tab_w1[ini.i] = Math.Sin(3.14 * ini.var_f_1 / 10 * (-ini.t - x) / ini.var_v_1 + ini.var_faza_1) * ini.var_a_1;
                            if (ini.w_f_1 == true)
                            {
                                chart1.Series["Series1"].Points.AddXY(x, tab_w1[ini.i]);
                                if (radio_pkt_f1.Checked == true)
                                {
                                    if (ini.active_pkt == ini.var_pkt)
                                    {
                                        chart1.Series["Series5"].Points.AddXY(0, tab_w1[ini.i]);
                                        chart1.Series["Series5"].Points.AddXY(x, tab_w1[ini.i]);
                                        wynik.Text = Math.Round(tab_w1[ini.i], 3).ToString();
                                    }
                                }
                            }
                            ini.i++;
                        }
                        ini.active_pkt = 0;
                    }
                    else if (ini.var_v_1 == 0)
                    {
                        ini.i = 0;
                        for (int x = 0; x < ini.var_max_x + ini.var_krok; x += ini.var_krok)
                        {
                            tab_w1[ini.i] = 0;
                            ini.i++;
                        }
                    }
                    else if (ini.var_v_1 > 0)
                    {
                        ini.i = 0;
                        for (int x = 0; x < ini.var_max_x + ini.var_krok; x += ini.var_krok)
                        {
                            ini.active_pkt++;
                            tab_w1[ini.i] = Math.Sin(3.14 * ini.var_f_1 / 10 * (ini.t - x) / ini.var_v_1 + ini.var_faza_1) * ini.var_a_1;
                            if (ini.w_f_1 == true)
                            {
                                chart1.Series["Series1"].Points.AddXY(x, tab_w1[ini.i]);
                                if (radio_pkt_f1.Checked == true)
                                {
                                    if (ini.active_pkt == ini.var_pkt)
                                    {
                                        chart1.Series["Series5"].Points.AddXY(0, tab_w1[ini.i]);
                                        chart1.Series["Series5"].Points.AddXY(x, tab_w1[ini.i]);
                                        wynik.Text = Math.Round(tab_w1[ini.i], 3).ToString();
                                    }
                                }
                            }
                            ini.i++;
                        }
                        ini.active_pkt = 0;
                    }

                    if (ini.var_v_2 < 0)
                    {
                        ini.v2 = ini.var_v_2 * (-1);
                        ini.i = 0;
                        for (int x = 0; x < ini.var_max_x + ini.var_krok; x += ini.var_krok)
                        {
                            ini.active_pkt++;
                            tab_w2[ini.i] = Math.Sin(3.14 * ini.var_f_2 / 10 * (-ini.t - x) / ini.var_v_2 + ini.var_faza_2) * ini.var_a_2;
                            if (ini.w_f_2 == true)
                            {
                                chart1.Series["Series2"].Points.AddXY(x, tab_w2[ini.i]);
                                if (radio_pkt_f2.Checked == true)
                                {
                                    if (ini.active_pkt == ini.var_pkt)
                                    {
                                        chart1.Series["Series5"].Points.AddXY(0, tab_w2[ini.i]);
                                        chart1.Series["Series5"].Points.AddXY(x, tab_w2[ini.i]);
                                        wynik.Text = Math.Round(tab_w2[ini.i], 3).ToString();
                                    }
                                }
                            }
                            ini.i++;
                        }
                        ini.active_pkt = 0;
                    }
                    else if (ini.var_v_2 == 0)
                    {
                        ini.i = 0;
                        for (int x = 0; x < ini.var_max_x + ini.var_krok; x += ini.var_krok)
                        {
                            tab_w2[ini.i] = 0;
                            ini.i++;
                        }
                    }
                    else if (ini.var_v_2 > 0)
                    {
                        ini.i = 0;
                        for (int x = 0; x < ini.var_max_x + ini.var_krok; x += ini.var_krok)
                        {
                            ini.active_pkt++;
                            tab_w2[ini.i] = Math.Sin(3.14 * ini.var_f_2 / 10 * (ini.t - x) / ini.var_v_2 + ini.var_faza_2) * ini.var_a_2;
                            if (ini.w_f_2 == true)
                            {
                                chart1.Series["Series2"].Points.AddXY(x, tab_w2[ini.i]);
                                if (radio_pkt_f2.Checked == true)
                                {
                                    if (ini.active_pkt == ini.var_pkt)
                                    {
                                        chart1.Series["Series5"].Points.AddXY(0, tab_w2[ini.i]);
                                        chart1.Series["Series5"].Points.AddXY(x, tab_w2[ini.i]);
                                        wynik.Text = Math.Round(tab_w2[ini.i], 3).ToString();
                                    }
                                }
                            }
                            ini.i++;
                        }
                        ini.active_pkt = 0;
                    }

                    if (ini.w_f_w == true)
                    {
                        ini.i = 0;
                        for (int x = 0; x < ini.var_max_x + ini.var_krok; x += ini.var_krok)
                        {
                            ini.active_pkt++;
                            chart1.Series["Series3"].Points.AddXY(x, tab_w1[ini.i] + tab_w2[ini.i]);
                            if (radio_pkt_wypadkowa.Checked == true)
                            {
                                if (ini.active_pkt == ini.var_pkt)
                                {
                                    ini.tmp = tab_w1[ini.i] + tab_w2[ini.i];
                                    chart1.Series["Series5"].Points.AddXY(0, ini.tmp);
                                    chart1.Series["Series5"].Points.AddXY(x, ini.tmp);
                                    wynik.Text = Math.Round(ini.tmp, 3).ToString();
                                }
                            }
                            ini.i++;
                        }
                        ini.active_pkt = 0;
                    }
                }
                ini.t += ini.var_krok_t;
            }
        }

        public void rysuj2()
        {
            double[] tab_w1 = new double[ini.var_max_x + ini.var_krok + 1];
            double[] tab_w2 = new double[ini.var_max_x + ini.var_krok + 1];
            int i = 0;
            if (ini.s_v == true)
            {
                ini.v1 = ini.var_bazowa_v;
                ini.v2 = ini.v1 * ini.var_stosunek_v_;
            }
            else
            {
                ini.v1 = ini.var_v_1;
                ini.v2 = ini.var_v_2;
            }

            if (ini.s_f == true)
            {
                ini.f1 = ini.var_bazowa_f;
                ini.f2 = ini.f1 * ini.var_stosunek_f_;
            }
            else
            {
                ini.f1 = ini.var_f_1;
                ini.f2 = ini.var_f_2;
            }

            if (ini.s_a == true)
            {
                ini.a1 = ini.var_bazowa_a;
                ini.a2 = ini.a1 * ini.var_stosunek_a_;
            }
            else
            {
                ini.a1 = ini.var_a_1;
                ini.a2 = ini.var_a_2;
            }

            ini.active_pkt = 0;
            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            chart1.Series["Series5"].Points.Clear();
            if (check_stosunek_v.Checked == false)
            {
                if (ini.var_v_1 > 0)
                {
                    if (check_pkt.Checked == false)
                    {
                        i = 0;
                        for (int x = 0; x < ini.var_max_x + ini.var_krok; x += ini.var_krok)
                        {
                            tab_w1[i] = Math.Sin(3.14 * ini.f1 / 10 * (ini.t - x) / ini.v1 + ini.var_faza_1) * ini.a1;
                            if (ini.w_f_1 == true)
                            {
                                chart1.Series["Series1"].Points.AddXY(x, tab_w1[i]);
                            }
                            i++;
                        }
                    }
                    else
                    {
                        i = 0;
                        for (int x = 0; x < ini.var_max_x + ini.var_krok; x += ini.var_krok)
                        {
                            ini.active_pkt++;
                            tab_w1[i] = Math.Sin(3.14 * ini.f1 / 10 * (ini.t - x) / ini.v1 + ini.var_faza_1) * ini.a1;
                            if (ini.w_f_1 == true)
                            {
                                chart1.Series["Series1"].Points.AddXY(x, tab_w1[i]);
                                if (radio_pkt_f1.Checked == true)
                                {
                                    if (ini.active_pkt == ini.var_pkt)
                                    {
                                        chart1.Series["Series5"].Points.AddXY(0, tab_w1[i]);
                                        chart1.Series["Series5"].Points.AddXY(x, tab_w1[i]);
                                        wynik.Text = Math.Round(tab_w1[i], 3).ToString();
                                    }
                                }
                            }
                            i++;
                        }
                        ini.active_pkt = 0;
                    }
                }
                else
                {
                    if (check_pkt.Checked == false)
                    {
                        i = 0;
                        for (int x = 0; x < ini.var_max_x + ini.var_krok; x += ini.var_krok)
                        {
                            tab_w1[i] = Math.Sin(3.14 * ini.f1 / 10 * (-ini.t - x) / ini.v1 + ini.var_faza_1) * ini.a1;
                            if (ini.w_f_1 == true)
                            {
                                chart1.Series["Series1"].Points.AddXY(x, tab_w1[i]);
                            }
                            i++;
                        }
                    }
                    else
                    {
                        i = 0;
                        for (int x = 0; x < ini.var_max_x + ini.var_krok; x += ini.var_krok)
                        {
                            ini.active_pkt++;
                            tab_w1[i] = Math.Sin(3.14 * ini.f1 / 10 * (-ini.t - x) / ini.v1 + ini.var_faza_1) * ini.a1;
                            if (ini.w_f_1 == true)
                            {
                                chart1.Series["Series1"].Points.AddXY(x, tab_w1[i]);
                                if (radio_pkt_f1.Checked == true)
                                {
                                    if (ini.active_pkt == ini.var_pkt)
                                    {
                                        chart1.Series["Series5"].Points.AddXY(0, tab_w1[i]);
                                        chart1.Series["Series5"].Points.AddXY(x, tab_w1[i]);
                                        wynik.Text = Math.Round(tab_w1[i], 3).ToString();
                                    }
                                }
                            }
                            i++;
                        }
                        ini.active_pkt = 0;
                    }
                }

                if (ini.var_v_2 > 0)
                {
                    if (check_pkt.Checked == false)
                    {
                        i = 0;
                        for (int x = 0; x < ini.var_max_x + ini.var_krok; x += ini.var_krok)
                        {
                            tab_w2[i] = Math.Sin(3.14 * ini.f2 / 10 * (ini.t - x) / ini.v2 + ini.var_faza_2) * ini.a2;
                            if (ini.w_f_2 == true)
                            {
                                chart1.Series["Series2"].Points.AddXY(x, tab_w2[i]);
                            }
                            i++;
                        }
                    }
                    else
                    {
                        i = 0;
                        for (int x = 0; x < ini.var_max_x + ini.var_krok; x += ini.var_krok)
                        {
                            ini.active_pkt++;
                            tab_w2[i] = Math.Sin(3.14 * ini.f2 / 10 * (ini.t - x) / ini.v2 + ini.var_faza_2) * ini.a2;
                            if (ini.w_f_2 == true)
                            {
                                chart1.Series["Series2"].Points.AddXY(x, tab_w2[i]);
                                if (radio_pkt_f2.Checked == true)
                                {
                                    if (ini.active_pkt == ini.var_pkt)
                                    {
                                        chart1.Series["Series5"].Points.AddXY(0, tab_w2[i]);
                                        chart1.Series["Series5"].Points.AddXY(x, tab_w2[i]);
                                        wynik.Text = Math.Round(tab_w2[i], 3).ToString();
                                    }
                                }
                            }
                            i++;
                        }
                        ini.active_pkt = 0;
                    }
                }
                else
                {
                    if (check_pkt.Checked == false)
                    {
                        i = 0;
                        for (int x = 0; x < ini.var_max_x + ini.var_krok; x += ini.var_krok)
                        {
                            tab_w2[i] = Math.Sin(3.14 * ini.f2 / 10 * (-ini.t - x) / ini.v2 + ini.var_faza_2) * ini.a2;
                            if (ini.w_f_2 == true)
                            {
                                chart1.Series["Series2"].Points.AddXY(x, tab_w2[i]);
                            }
                            i++;
                        }
                    }
                    else
                    {
                        i = 0;
                        for (int x = 0; x < ini.var_max_x + ini.var_krok; x += ini.var_krok)
                        {
                            ini.active_pkt++;
                            tab_w2[i] = Math.Sin(3.14 * ini.f2 / 10 * (-ini.t - x) / ini.v2 + ini.var_faza_2) * ini.a2;
                            if (ini.w_f_2 == true)
                            {
                                chart1.Series["Series2"].Points.AddXY(x, tab_w2[i]);
                                if (radio_pkt_f2.Checked == true)
                                {
                                    if (ini.active_pkt == ini.var_pkt)
                                    {
                                        chart1.Series["Series5"].Points.AddXY(0, tab_w2[i]);
                                        chart1.Series["Series5"].Points.AddXY(x, tab_w2[i]);
                                        wynik.Text = Math.Round(tab_w2[i], 3).ToString();
                                    }
                                }
                            }
                            i++;
                        }
                        ini.active_pkt = 0;
                    }
                }

                if (ini.w_f_w == true)
                {
                    i = 0;
                    for (int x = 0; x < ini.var_max_x + ini.var_krok; x += ini.var_krok)
                    {
                        ini.active_pkt++;
                        chart1.Series["Series3"].Points.AddXY(x, tab_w1[i] + tab_w2[i]);
                        if (radio_pkt_wypadkowa.Checked == true)
                        {
                            if (ini.active_pkt == ini.var_pkt)
                            {
                                ini.tmp = tab_w1[i] + tab_w2[i];
                                chart1.Series["Series5"].Points.AddXY(0, ini.tmp);
                                chart1.Series["Series5"].Points.AddXY(x, ini.tmp);
                                wynik.Text = Math.Round(ini.tmp, 3).ToString();
                            }
                        }
                        i++;
                    }
                    ini.active_pkt = 0;
                }
                ini.t += ini.var_krok_t;
            }
            else
            {
                if (check_pkt.Checked == false)
                {
                    for (int x = 0; x < ini.var_max_x + ini.var_krok; x += ini.var_krok)
                    {
                        ini.tmp1 = Math.Sin(3.14 * ini.f1 / 10 * (ini.t - x) / ini.v1 + ini.var_faza_1) * ini.a1;
                        if (ini.w_f_1 == true)
                        {
                            chart1.Series["Series1"].Points.AddXY(x, ini.tmp1);
                        }

                        ini.tmp2 = Math.Sin(3.14 * ini.f2 / 10 * (ini.t - x) / ini.v2 + ini.var_faza_2) * ini.a2;
                        if (ini.w_f_2 == true)
                        {
                            chart1.Series["Series2"].Points.AddXY(x, ini.tmp2);
                        }

                        if (ini.w_f_w == true)
                        {
                            chart1.Series["Series3"].Points.AddXY(x, ini.tmp1 + ini.tmp2);
                        }
                    }
                    ini.t += ini.var_krok_t;
                }
                else
                {
                    for (int x = 0; x < ini.var_max_x + ini.var_krok; x += ini.var_krok)
                    {
                        ini.active_pkt++;
                        ini.tmp1 = Math.Sin(3.14 * ini.f1 / 10 * (ini.t - x) / ini.v1 + ini.var_faza_1) * ini.a1;
                        if (ini.w_f_1 == true)
                        {
                            chart1.Series["Series1"].Points.AddXY(x, ini.tmp1);
                            if (radio_pkt_f1.Checked == true)
                            {
                                if (ini.active_pkt == ini.var_pkt)
                                {
                                    chart1.Series["Series5"].Points.AddXY(0, ini.tmp1);
                                    chart1.Series["Series5"].Points.AddXY(x, ini.tmp1);
                                    wynik.Text = Math.Round(ini.tmp1, 3).ToString();
                                }
                            }
                        }

                        ini.tmp2 = Math.Sin(3.14 * ini.f2 / 10 * (ini.t - x) / ini.v2 + ini.var_faza_2) * ini.a2;
                        if (ini.w_f_2 == true)
                        {
                            chart1.Series["Series2"].Points.AddXY(x, ini.tmp2);
                            if (radio_pkt_f2.Checked == true)
                            {
                                if (ini.active_pkt == ini.var_pkt)
                                {
                                    chart1.Series["Series5"].Points.AddXY(0, ini.tmp2);
                                    chart1.Series["Series5"].Points.AddXY(x, ini.tmp2);
                                    wynik.Text = Math.Round(ini.tmp2, 3).ToString();
                                }
                            }
                        }

                        if (ini.w_f_w == true)
                        {
                            chart1.Series["Series3"].Points.AddXY(x, ini.tmp1 + ini.tmp2);
                            if (radio_pkt_wypadkowa.Checked == true)
                            {
                                if (ini.active_pkt == ini.var_pkt)
                                {
                                    ini.tmp = ini.tmp1 + ini.tmp2;
                                    chart1.Series["Series5"].Points.AddXY(0, ini.tmp);
                                    chart1.Series["Series5"].Points.AddXY(x, ini.tmp);
                                    wynik.Text = Math.Round(ini.tmp, 3).ToString();
                                }
                            }
                        }
                    }
                    ini.t += ini.var_krok_t;
                }
            }
        }

        public void auto_var()
        {
            if (radio_show_w.Checked == true)
            {
                ini.var_krok = 5;
                ini.var_krok_t = 5;
                ini.var_szybkosc_wyswietlania = 960;
            }
            if (radio_show_s.Checked == true)
            {
                ini.var_krok = 15;
                ini.var_krok_t = 10;
                ini.var_szybkosc_wyswietlania = 960;
            }
            if (radio_show_sz.Checked == true)
            {
                ini.var_krok = 20;
                ini.var_krok_t = 15;
                ini.var_szybkosc_wyswietlania = 960;
            }
            auto_size();
        }

        public void auto_size()
        {
            if (radio_m.Checked == true)
            {
                ini.var_wymiar_x = 680;
                ini.var_wymiar_y = 350;
            }
            if (radio_s.Checked == true)
            {
                ini.var_wymiar_x = 820;
                ini.var_wymiar_y = 450;
            }
            if (radio_d.Checked == true)
            {
                ini.var_wymiar_x = 1200;
                ini.var_wymiar_y = 600;
            }
        }

        public void wykres()
        {
            if (ini.sound)
            {
                return;
            }

            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series2"].Points.Clear();
            chart1.Series["Series3"].Points.Clear();
            chart1.Series["Series5"].Points.Clear();
            if (check_zaawansowane.Checked == true)
            {
                auto_var();
            }
            ini.m_pkt = ini.var_max_x / ini.var_krok;
           if(WindowState != FormWindowState.Maximized)
            {
                ClientSize = new System.Drawing.Size(ini.var_wymiar_x, ini.var_wymiar_y);
                chart1.Size = new System.Drawing.Size(ClientSize.Width, ini.var_wymiar_y - 186);
            }
            panel.Size = new System.Drawing.Size(ClientSize.Width, 186);
            chart1.ChartAreas[0].AxisY.Maximum = ini.var_max_y;
            chart1.ChartAreas[0].AxisY.Minimum = ini.var_min_y;
            chart1.ChartAreas[0].AxisX.Maximum = ini.var_max_x;
            chart1.Location = new System.Drawing.Point(1, 186);
            chart1.Series["Series4"].Points.AddXY(0, 0);
            chart1.Series["Series4"].Points.AddXY(ini.var_max_x, 0);
            timer1.Interval = 1000 - ini.var_szybkosc_wyswietlania;
            timer2.Interval = 1000 - ini.var_szybkosc_wyswietlania;
            if (ini.var_styl_1 == "Liniowy")
            {
                chart1.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            }
            else
            {
                chart1.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;

            }

            if (ini.var_styl_2 == "Liniowy")
            {
                chart1.Series["Series2"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            }
            else
            {
                chart1.Series["Series2"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;

            }

            if (ini.var_styl_3 == "Liniowy")
            {
                chart1.Series["Series3"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            }
            else
            {
                chart1.Series["Series3"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;

            }
            ini.tmp1 = 0;
            ini.tmp2 = 0;
            ini.tmp = 0;
            ini.t = 0;
        }

        public void sound_play()
        {
            btn_play.Enabled = false;
            var mStrm = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(mStrm);
            int formatChunkSize = 16;
            int headerSize = 8;
            short formatType = 1;
            short tracks = 2;
            int samplesPerSecond = 44100;
            short bitsPerSample = 16;
            int msDuration = track_s_t.Value * 1000;
            short frameSize = (short)(tracks * ((bitsPerSample + 7) / 8));
            int bytesPerSecond = samplesPerSecond * frameSize;
            int waveSize = 4;
            int samples = (int)((decimal)samplesPerSecond * msDuration / 1000);
            int dataChunkSize = samples * frameSize;
            int fileSize = waveSize + headerSize + formatChunkSize + headerSize + dataChunkSize;
            double theta = ini.var_s_f1 * 2 * Math.PI / (double)samplesPerSecond;
            double theta2 = ini.var_s_f2 * 2 * Math.PI / (double)samplesPerSecond;
            double amp = (double)ini.var_s_a1;
            double amp2 = (double)ini.var_s_a2;
            writer.Write(0x46464952);
            writer.Write(fileSize);
            writer.Write(0x45564157);
            writer.Write(0x20746D66);
            writer.Write(formatChunkSize);
            writer.Write(formatType);
            writer.Write(tracks);
            writer.Write(samplesPerSecond);
            writer.Write(bytesPerSecond);
            writer.Write(frameSize);
            writer.Write(bitsPerSample);
            writer.Write(0x61746164);
            writer.Write(dataChunkSize);
            List<short> shortArray = new List<short>();            
            if (check_s_w2.Checked==true)
            {
                if (check_stereo.Checked == true)
                {
                    for (uint i = 0; i < samples; i += 2)
                    {
                        shortArray.Add(0);
                        shortArray.Add(0);
                        shortArray.Add(0);
                        shortArray.Add(0);
                        for (int channel = 0; channel < 2; channel++)
                        {
                            if (channel == 0) shortArray[(int)i + channel] = Convert.ToInt16(amp * Math.Sin(theta * (double)i));
                            else shortArray[(int)i + channel] = Convert.ToInt16(amp2 * Math.Sin(theta2 * (double)i));
                        }
                    }
                    for (uint i = 0; i < samples * 2; i++)
                    {
                        writer.Write(shortArray[(int)i]);
                    }
                }
                else
                {
                    for (int step = 0; step < samples*2; step++)
                    {
                        ini.tmp1 = amp * Math.Sin(theta * (double)step);
                        ini.tmp2 = amp2 * Math.Sin(theta2 * (double)step);
                        writer.Write(Convert.ToInt16(ini.tmp1 + ini.tmp2));
                    }
                }
            }
            else
            {
                for (int step = 0; step < samples*2; step++)
                {
                    writer.Write(Convert.ToInt16(amp * Math.Sin(theta * (double)step)));
                }
            }
            mStrm.Seek(0, SeekOrigin.Begin);
            new System.Media.SoundPlayer(mStrm).Play();
            writer.Close();
            mStrm.Close();
            shortArray.Clear();
            btn_play.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            rysuj();
        }

        private void stop_start_Click(object sender, EventArgs e)
        {
            if (stop_start.Text == "START")
            {
                uruchom(true);
                stop_start.Text = "STOP";
            }
            else
            {
                uruchom(false);
                stop_start.Text = "START";
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            rysuj2();
        }

        private void zapisz_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void wczytaj_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text file |*.if";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string file_name = openFileDialog1.FileName;
                try
                {
                    if (File.Exists(file_name))
                    {
                        string[] tab_val = File.ReadAllLines(file_name);

                        check_fala_1.Checked = bool.Parse(tab_val[0]);
                        check_fala_2.Checked = bool.Parse(tab_val[1]);
                        check_fala_wypadkowa.Checked = bool.Parse(tab_val[2]);
                        radio_show_w.Checked = bool.Parse(tab_val[3]);
                        radio_show_s.Checked = bool.Parse(tab_val[4]);
                        radio_show_sz.Checked = bool.Parse(tab_val[5]);
                        radio_m.Checked = bool.Parse(tab_val[6]);
                        radio_s.Checked = bool.Parse(tab_val[7]);
                        radio_d.Checked = bool.Parse(tab_val[8]);
                        box_styl_1.SelectedItem = tab_val[9];
                        box_styl_2.SelectedItem = tab_val[10];
                        box_styl_3.SelectedItem = tab_val[11];
                        box_f_1.Text = tab_val[12];
                        box_f_2.Text = tab_val[13];
                        box_bazowe_f.Text = tab_val[14];
                        box_stosunek_f.Text = tab_val[15];
                        check_stosunek_f.Checked = bool.Parse(tab_val[16]);
                        box_a_1.Text = tab_val[17];
                        box_a_2.Text = tab_val[18];
                        box_a_bazowe.Text = tab_val[19];
                        box_a_stosunek.Text = tab_val[20];
                        check_stosunek_a.Checked = bool.Parse(tab_val[21]);
                        box_v_1.Text = tab_val[22];
                        box_v_2.Text = tab_val[23];
                        check_left1.Checked = bool.Parse(tab_val[24]);
                        check_right1.Checked = bool.Parse(tab_val[25]);
                        check_left2.Checked = bool.Parse(tab_val[26]);
                        check_right2.Checked = bool.Parse(tab_val[27]);
                        box_bazowe_v.Text = tab_val[28];
                        box_stosunek_v.Text = tab_val[29];
                        check_stosunek_v.Checked = bool.Parse(tab_val[30]);
                        box_faza_1.Text = tab_val[31];
                        box_faza_2.Text = tab_val[32];
                        box_pkt.Text = tab_val[33];
                        radio_pkt_f1.Checked = bool.Parse(tab_val[34]);
                        radio_pkt_f2.Checked = bool.Parse(tab_val[35]);
                        radio_pkt_wypadkowa.Checked = bool.Parse(tab_val[36]);
                        check_pkt.Checked = bool.Parse(tab_val[37]);
                        box_s_f1.Text = tab_val[38];
                        box_s_a1.Text = tab_val[39];
                        box_s_f2.Text = tab_val[40];
                        box_s_a2.Text = tab_val[41];
                        check_stereo.Checked = bool.Parse(tab_val[42]);
                        check_s_w2.Checked = bool.Parse(tab_val[43]);
                        box_max_y.Text = tab_val[44];
                        box_min_y.Text = tab_val[45];
                        box_max_x.Text = tab_val[46];
                        box_velocity_show.Text = tab_val[47];
                        box_poziom.Text = tab_val[48];
                        box_pion.Text = tab_val[49];
                        box_krok.Text = tab_val[50];
                        box_krok_t.Text = tab_val[51];
                        check_zaawansowane.Checked = bool.Parse(tab_val[52]);

                        wczytaj_var();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Nie udało się otworzyć pliku." + ex);
                }
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            List<string> save = new List<string>();
            save.Add(check_fala_1.Checked.ToString());
            save.Add(check_fala_2.Checked.ToString());
            save.Add(check_fala_wypadkowa.Checked.ToString());
            save.Add(radio_show_w.Checked.ToString());
            save.Add(radio_show_s.Checked.ToString());
            save.Add(radio_show_sz.Checked.ToString());
            save.Add(radio_m.Checked.ToString());
            save.Add(radio_s.Checked.ToString());
            save.Add(radio_d.Checked.ToString());
            save.Add(box_styl_1.SelectedItem.ToString());
            save.Add(box_styl_2.SelectedItem.ToString());
            save.Add(box_styl_3.SelectedItem.ToString());
            save.Add(box_f_1.Text);
            save.Add(box_f_2.Text);
            save.Add(box_bazowe_f.Text);
            save.Add(box_stosunek_f.Text);
            save.Add(check_stosunek_f.Checked.ToString());
            save.Add(box_a_1.Text);
            save.Add(box_a_2.Text);
            save.Add(box_a_bazowe.Text);
            save.Add(box_a_stosunek.Text);
            save.Add(check_stosunek_a.Checked.ToString());
            save.Add(box_v_1.Text);
            save.Add(box_v_2.Text);
            save.Add(check_left1.Checked.ToString());
            save.Add(check_right1.Checked.ToString());
            save.Add(check_left2.Checked.ToString());
            save.Add(check_right2.Checked.ToString());
            save.Add(box_bazowe_v.Text);
            save.Add(box_stosunek_v.Text);
            save.Add(check_stosunek_v.Checked.ToString());
            save.Add(box_faza_1.Text);
            save.Add(box_faza_2.Text);
            save.Add(box_pkt.Text);
            save.Add(radio_pkt_f1.Checked.ToString());
            save.Add(radio_pkt_f2.Checked.ToString());
            save.Add(radio_pkt_wypadkowa.Checked.ToString());
            save.Add(check_pkt.Checked.ToString());
            save.Add(box_s_f1.Text);
            save.Add(box_s_a1.Text);
            save.Add(box_s_f2.Text);
            save.Add(box_s_a2.Text);
            save.Add(check_stereo.Checked.ToString());
            save.Add(check_s_w2.Checked.ToString());
            save.Add(box_max_y.Text);
            save.Add(box_min_y.Text);
            save.Add(box_max_x.Text);
            save.Add(box_velocity_show.Text);
            save.Add(box_poziom.Text);
            save.Add(box_pion.Text);
            save.Add(box_krok.Text);
            save.Add(box_krok_t.Text);
            save.Add(check_zaawansowane.Checked.ToString());

            File.WriteAllLines(saveFileDialog1.FileName, save);
        }

        private void tabPage9_Enter(object sender, EventArgs e)
        {
            panel.Height = ClientSize.Height;
            ini.sound = true;
            btn_more.Visible = false;
        }

        private void tabPage9_Leave(object sender, EventArgs e)
        {
            panel.Height = 187;
            ini.sound = false;
            btn_more.Visible = true;
        }

        private void track_s_t_Scroll(object sender, EventArgs e)
        {
            l_t.Text = track_s_t.Value.ToString() + " s";
            ini.var_s_t = track_s_t.Value;
        }

        private void check_s_w2_CheckedChanged(object sender, EventArgs e)
        {
            if (check_s_w2.Checked == true)
            {
                group_s_fala2.Enabled = true;
            }
            else
            {
                group_s_fala2.Enabled = false;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            sound_play();
        }

        private void button46_Click(object sender, EventArgs e)
        {
            Process update = new Process();
            update.StartInfo.FileName = "update.exe";
            update.Start();
            Application.Exit();
        }

        private void reset_Click(object sender, EventArgs e)
        {
            resetAll();
        }

        public void resetAll()
        {
            WindowState = FormWindowState.Normal;
            groupBox15.Enabled = true;
            ini = new InitialVar();
            zmienne_start();
            check_stosunek(false);
            uruchom(false);
        }

        private void btn_more_Click(object sender, EventArgs e)
        {
            if (ini.more)
            {
                ini.tmp_height = panel.Height;
                ini.more = false;
                panel.Height = 0;
                chart1.Location = new System.Drawing.Point(0, 0);
                chart1.Size = new System.Drawing.Size(ClientSize.Width, ClientSize.Height);
                btn_more.Location = new System.Drawing.Point(2, 2);
            }
            else
            {
                ini.more = true;
                panel.Height = ini.tmp_height;
                chart1.Location = new System.Drawing.Point(0, 186);
                chart1.Size = new System.Drawing.Size(ClientSize.Width, ClientSize.Height - 186);
                btn_more.Location = new System.Drawing.Point(2, 187);
            }
        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            if (this.WindowState != mLastState)
            {
                mLastState = this.WindowState;
                OnWindowStateChanged(e);
            }
            base.OnClientSizeChanged(e);
        }

        protected void OnWindowStateChanged(EventArgs e)
        {
            if(WindowState == FormWindowState.Maximized)
            {
                ini.tmp_screenX = ini.var_wymiar_x;
                ini.tmp_screenY = ini.var_wymiar_y;
                ini.var_wymiar_x = ClientSize.Width;
                ini.var_wymiar_y = ClientSize.Height - 186;
                groupBox15.Enabled = false;
                if (!ini.more)
                {
                    chart1.Location = new System.Drawing.Point(0, 0);
                    ini.var_wymiar_y = ClientSize.Height;
                    chart1.Size = new System.Drawing.Size(ini.m_wymiar_x, ini.var_wymiar_y);
                    panel.Size = new System.Drawing.Size(ini.m_wymiar_x, 0);
                    btn_more.Location = new System.Drawing.Point(2, 2);
                }
                else
                { 
                    chart1.Location = new System.Drawing.Point(0, 186);
                    chart1.Size = new System.Drawing.Size(ini.m_wymiar_x, ini.var_wymiar_y);
                    panel.Size = new System.Drawing.Size(ini.m_wymiar_x, 186);
                    btn_more.Location = new System.Drawing.Point(2, 187);
                }
            }
            else
            {
                ini.var_wymiar_x = ini.tmp_screenX;
                ini.var_wymiar_y = ini.tmp_screenY;
                groupBox15.Enabled = true;
                if (!ini.more)
                {
                    btn_more.Location = new System.Drawing.Point(2, 2);
                    panel.Size = new System.Drawing.Size(ini.tmp_screenX, 0);
                    chart1.Location = new System.Drawing.Point(0, 0);
                    chart1.Size = new System.Drawing.Size(ini.var_wymiar_x, ini.var_wymiar_y);
                }
                else
                {
                    btn_more.Location = new System.Drawing.Point(2, 187);
                    panel.Size = new System.Drawing.Size(ini.tmp_screenX, 186);
                    chart1.Location = new System.Drawing.Point(1, 186);
                    chart1.Size = new System.Drawing.Size(ini.tmp_screenX, ini.tmp_screenY - 186);
                }
            }
            if (ini.sound)
            {
                panel.Size = new System.Drawing.Size(ClientSize.Width, ClientSize.Height);
            }
        }
    }
}
