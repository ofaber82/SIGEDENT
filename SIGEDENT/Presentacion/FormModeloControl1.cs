﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class FormModeloControl1 : Form
    {
        private static FormModeloControl1 iform_modelocontrol1;
        private Entidades.Modelo modelo; //descomentar y declarar la entidad con la cual se trabajará
        
        public FormModeloControl1()
        {
            InitializeComponent();
        }

        private void FormModeloControl1_Load(object sender, EventArgs e)
        {
            dgv_vista.AutoGenerateColumns = false;
            //Elegir proporcion para cada columna con respecto al tamaño de la pantalla //descomentar la siguiente linea
            //dgv_vista.Columns[0].Width = Convert.ToInt32((Screen.PrimaryScreen.Bounds.Width / 4.8));            
            LLenar_DataGridView("");
            Activar_Panel(false); //Inicialmente activamos el panel busqueda            
        }

        public static FormModeloControl1 FormModeloControl1_Instanciar()
        {
            if (iform_modelocontrol1 == null || iform_modelocontrol1.IsDisposed == true)
            {
                iform_modelocontrol1 = new FormModeloControl1();
            }
            iform_modelocontrol1.BringToFront();
            return iform_modelocontrol1;
        }

        private void Activar_Panel(bool estado)
        {
            pan_registro.Enabled = estado;
            pan_vista.Enabled = !estado;
            if (estado)
                txb_campo_prueba.Focus();
            else
                txb_buscar.Focus();
        }

        private void LLenar_DataGridView(string busqueda)
        {
            //modificar el metodo segun la tabla que queramos mostrar en el dgv_vista
            try { dgv_vista.DataSource = Datos.DArea.Area_Seleccionar_Filtro_Lista("nombre", busqueda); }
            catch (Exception ex) { MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void Detallar_Elegido()
        {
            //llenar las cajas de texto del panel pan_registro con las propiedades de la clase instanciada actual
        }

        private void txb_buscar_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                LLenar_DataGridView(txb_buscar.Text);
        }

        private void txb_buscar_TextChanged(object sender, EventArgs e)
        {
            /*Implementar el filtro en el Datagrdview pero sin consultar la base de datos,
             * si no trabajar con el datatable obtenido al abrir el formulario o despues
             * de insertar o actuallizar */
        }

        private void btn_nuevo_Click(object sender, EventArgs e)
        {
            //area = null;
            //txb_nombre.Clear();
            Activar_Panel(true);
        }

        private void btn_editar_Click(object sender, EventArgs e)
        {
            if (dgv_vista.CurrentRow != null)
            {
                //instanciamos el objeto entidad con la fila elegida en el dgv_vista
                //area = (Entidades.Area)dgv_vista.CurrentRow.DataBoundItem;
                Detallar_Elegido();
            }
            else
                MessageBox.Show("Debe elegir una fila en la relacion de areas");
        }

        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Desea Eliminar el Registro de la Base de Datos", "Confirmar Eliminacion", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                if (dgv_vista.CurrentRow != null)
                {
                    //try { Datos.DArea.Area_Eliminar(Convert.ToInt16(dgv_vista.CurrentRow.Cells["Id"].Value)); }
                    //catch (Exception ex) { MessageBox.Show(ex.Message); }
                    LLenar_DataGridView("");
                }
                else
                    MessageBox.Show("Debe elegir una fila en la relacion de areas");
            }
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            /* si no usamos la capa Negocio, entonces debemos validar antes de este evento */
            if ( txb_campo_prueba.Text != "" && txb_campo_prueba.Text.Length > 2)
            { //nombre no vacio y mayo de 2 cifras
                if (modelo == null)
                { // el area a grabar no fue elgida del dgv, entonces instanciamos el objeto area indicando id=0 para que el sp_area_grabar realice un registro nuevo
                    modelo = new Entidades.Modelo();
                    modelo.id = Convert.ToInt16(0);
                    modelo.nombre = txb_campo_prueba.Text;
                }
                //descomentar el try catch
                //try { Datos.DArea.Area_Grabar(area); }
                //catch (Exception ex) { MessageBox.Show(ex.Message); }
                Activar_Panel(false);
                LLenar_DataGridView("");
            }
            else
                MessageBox.Show("Verifique los datos por favor");
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            Activar_Panel(false);
        }
    }
}
