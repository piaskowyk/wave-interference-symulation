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
    public partial class Form1
    {
        private void button26_Click(object sender, EventArgs e)
        {
            button_change_min(-1, box_f_1, ini.m_f_1, ini.f_1);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            button_change_min(1, box_f_1, ini.m_f_1, ini.f_1);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            button_change_min(-1, box_f_2, ini.m_f_2, ini.f_2);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            button_change_min(1, box_f_2, ini.m_f_2, ini.f_2);
        }

        private void button30_Click(object sender, EventArgs e)
        {
            button_change_min(-1, box_bazowe_f, ini.m_bazowa_f, ini.bazowa_f);
        }

        private void button29_Click(object sender, EventArgs e)
        {
            button_change_min(1, box_bazowe_f, ini.m_bazowa_f, ini.bazowa_f);
        }

        private void button32_Click(object sender, EventArgs e)
        {
            ini.n_tmp = -0.1;
            button_change_min(ini.n_tmp, box_stosunek_f, ini.m_stosunek_f_, ini.stosunek_f_);
        }

        private void button31_Click(object sender, EventArgs e)
        {
            ini.n_tmp = 0.1;
            button_change_min(ini.n_tmp, box_stosunek_f, ini.m_stosunek_f_, ini.stosunek_f_);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            button_change_min(-1, box_a_1, ini.m_a_1, ini.a_1);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            button_change_min(1, box_a_1, ini.m_a_1, ini.a_1);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            button_change_min(-1, box_a_2, ini.m_a_2, ini.a_2);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            button_change_min(1, box_a_2, ini.m_a_2, ini.a_2);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            button_change_min(-1, box_a_bazowe, ini.m_bazowa_a, ini.bazowa_a);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            button_change_min(1, box_a_bazowe, ini.m_bazowa_a, ini.bazowa_a);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            ini.n_tmp = -0.1;
            button_change_min(ini.n_tmp, box_a_stosunek, ini.m_stosunek_a_, ini.stosunek_a_);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            ini.n_tmp = 0.1;
            button_change_min(ini.n_tmp, box_a_stosunek, ini.m_stosunek_a_, ini.stosunek_a_);
        }

        private void minus_v_1_Click(object sender, EventArgs e)
        {
            button_change_min(-1, box_v_1, ini.m_v_1, ini.minimum_v_1);
        }

        private void plus_v_1_Click(object sender, EventArgs e)
        {
            button_change_min(1, box_v_1, ini.m_v_1, ini.minimum_v_1);
        }

        private void minus_v_2_Click(object sender, EventArgs e)
        {
            button_change_min(-1, box_v_2, ini.m_v_2, ini.minimum_v_2);
        }

        private void plus_v_2_Click(object sender, EventArgs e)
        {
            button_change_min(1, box_v_2, ini.m_v_2, ini.minimum_v_2);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            button_change_min(-1, box_bazowe_v, ini.m_bazowa_v, ini.bazowa_v);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            button_change_min(1, box_bazowe_v, ini.m_bazowa_v, ini.bazowa_v);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            ini.n_tmp = -0.1;
            button_change_min(ini.n_tmp, box_stosunek_v, ini.m_stosunek_v_, ini.stosunek_v_);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            ini.n_tmp = 0.1;
            button_change_min(ini.n_tmp, box_stosunek_v, ini.m_stosunek_v_, ini.stosunek_v_);
        }

        private void button35_Click(object sender, EventArgs e)
        {
            button_change_min(-1, box_faza_1, ini.m_faza_1, ini.faza_1);
        }

        private void button36_Click(object sender, EventArgs e)
        {
            button_change_min(1, box_faza_1, ini.m_faza_1, ini.faza_1);
        }

        private void button33_Click(object sender, EventArgs e)
        {
            button_change_min(-1, box_faza_2, ini.m_faza_2, ini.faza_2);
        }

        private void button34_Click(object sender, EventArgs e)
        {
            button_change_min(1, box_faza_2, ini.m_faza_2, ini.faza_2);
        }

        private void minus_max_y_Click(object sender, EventArgs e)
        {
            button_change(-1, box_max_y, ini.m_max_y, ini.max_y);
        }

        private void plus_max_y_Click(object sender, EventArgs e)
        {
            button_change(1, box_max_y, ini.m_max_y, ini.max_y);
        }

        private void minus_min_y_Click(object sender, EventArgs e)
        {
            button_change(-1, box_min_y, ini.m_min_y, ini.min_y);
        }

        private void plus_min_y_Click(object sender, EventArgs e)
        {
            button_change(1, box_min_y, ini.m_min_y, ini.min_y);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button_change(-1, box_max_x, ini.m_max_x, ini.max_x);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button_change(1, box_max_x, ini.m_max_x, ini.max_x);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button_change(-1, box_pion, ini.m_wymiar_y, ini.wymiar_y);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button_change(1, box_pion, ini.m_wymiar_y, ini.wymiar_y);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button_change(-1, box_poziom, ini.m_wymiar_x, ini.wymiar_x);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button_change(1, box_poziom, ini.m_wymiar_x, ini.wymiar_x);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            button_change_min(-1, box_krok, ini.m_krok, ini.krok);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            button_change_min(1, box_krok, ini.m_krok, ini.krok);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button_change_min(-1, box_krok_t, ini.m_krok_t, ini.krok);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            button_change_min(1, box_krok_t, ini.m_krok_t, ini.krok);
        }

        private void button39_Click(object sender, EventArgs e)
        {
            button_change_min(-1, box_velocity_show, ini.m_szybkosc_wyswietlania, ini.szybkosc_wyswietlania);
        }

        private void button40_Click(object sender, EventArgs e)
        {
            button_change_min(1, box_velocity_show, ini.m_szybkosc_wyswietlania, ini.szybkosc_wyswietlania);
        }

        private void minus_pkt_Click(object sender, EventArgs e)
        {
            button_change_min(-1, box_pkt, ini.m_pkt, ini.pkt);
        }

        private void plus_pkt_Click(object sender, EventArgs e)
        {
            button_change_min(1, box_pkt, ini.m_pkt, ini.pkt);
        }

        private void button43_Click(object sender, EventArgs e)
        {
            button_change(-1, box_s_a2, ini.m_s_a2, ini.s_a2);
        }

        private void button42_Click(object sender, EventArgs e)
        {
            button_change(1, box_s_a2, ini.m_s_a2, ini.s_a2);
        }

        private void button45_Click(object sender, EventArgs e)
        {
            button_change(-1, box_s_f2, ini.m_s_f2, ini.s_f2);
        }

        private void button44_Click(object sender, EventArgs e)
        {
            button_change(1, box_s_f2, ini.m_s_f2, ini.s_f2);
        }

        private void button41_Click(object sender, EventArgs e)
        {
            button_change(-1, box_s_a1, ini.m_s_a1, ini.s_a1);
        }

        private void button38_Click(object sender, EventArgs e)
        {
            button_change(1, box_s_a1, ini.m_s_a1, ini.s_a1);
        }

        private void button37_Click(object sender, EventArgs e)
        {
            button_change(-1, box_s_f1, ini.m_s_f1, ini.s_f1);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            button_change(1, box_s_f1, ini.m_s_f1, ini.s_f1);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////

        private void check_stosunek_f_Click(object sender, EventArgs e)
        {
            check_stosunek();
        }

        private void check_stosunek_a_Click(object sender, EventArgs e)
        {
            check_stosunek();
        }

        private void check_stosunek_v_Click(object sender, EventArgs e)
        {
            check_stosunek();
        }

        private void check_zaawansowane_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            wczytaj_var();
            check_stosunek(false);
            wykres();
        }

        private void check_pkt_CheckedChanged(object sender, EventArgs e)
        {
            check_stosunek();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        private void box_styl_1_SelectedValueChanged(object sender, EventArgs e)
        {
            wczytaj_var();
        }

        private void box_styl_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            wczytaj_var();
        }

        private void box_styl_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            wczytaj_var();
        }

        private void check_fala_1_MouseCaptureChanged(object sender, EventArgs e)
        {
            wczytaj_var();
        }

        private void check_fala_2_MouseCaptureChanged(object sender, EventArgs e)
        {
            wczytaj_var();
        }

        private void check_fala_wypadkowa_MouseCaptureChanged(object sender, EventArgs e)
        {
            wczytaj_var();
        }

        private void track_r_f1_Scroll(object sender, EventArgs e)
        {
            box_faza_1.Text = track_r_f1.Value.ToString();
            wczytaj_var();
        }

        private void track_r_f2_Scroll(object sender, EventArgs e)
        {
            box_faza_2.Text = track_r_f2.Value.ToString();
            wczytaj_var();
        }

        private void box_styl_2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            wczytaj_var();
        }

        private void radio_show_w_CheckedChanged(object sender, EventArgs e)
        {
            wczytaj_var();
        }

        private void radio_show_s_CheckedChanged(object sender, EventArgs e)
        {
            wczytaj_var();
        }

        private void radio_show_sz_CheckedChanged(object sender, EventArgs e)
        {
            wczytaj_var();
        }

        private void tabControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                wczytaj_var();
            }
        }

        private void check_left1_CheckedChanged(object sender, EventArgs e)
        {
            wczytaj_var();
        }

        private void check_right1_CheckedChanged(object sender, EventArgs e)
        {
            wczytaj_var();
        }

        private void check_left2_CheckedChanged(object sender, EventArgs e)
        {
            wczytaj_var();
        }

        private void check_right2_CheckedChanged(object sender, EventArgs e)
        {
            wczytaj_var();
        }

        private void radio_pkt_f1_CheckedChanged(object sender, EventArgs e)
        {
            wczytaj_var();
        }

        private void radio_pkt_f2_CheckedChanged(object sender, EventArgs e)
        {
            wczytaj_var();
        }

        private void radio_pkt_wypadkowa_CheckedChanged(object sender, EventArgs e)
        {
            wczytaj_var();
        }

        private void track_s_f1_Scroll(object sender, EventArgs e)
        {
            box_s_f1.Text = track_s_f1.Value.ToString();
            wczytaj_var();
        }

        private void track_s_a1_Scroll(object sender, EventArgs e)
        {
            box_s_a1.Text = track_s_a1.Value.ToString();
            wczytaj_var();
        }

        private void track_s_f2_Scroll(object sender, EventArgs e)
        {
            box_s_f2.Text = track_s_f2.Value.ToString();
            wczytaj_var();
        }

        private void track_s_a2_Scroll(object sender, EventArgs e)
        {
            box_s_a2.Text = track_s_a2.Value.ToString();
            wczytaj_var();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        private void radio_m_CheckedChanged(object sender, EventArgs e)
        {
            wykres();
        }

        private void radio_s_CheckedChanged(object sender, EventArgs e)
        {
            wykres();
        }

        private void radio_d_CheckedChanged(object sender, EventArgs e)
        {
            wykres();
        }
    }
}
