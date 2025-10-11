using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace PokemonVS1
{
    public partial class frmAtlaPokemon : Form
    {
        public frmAtlaPokemon()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close(); //Botón para cancelar

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Este botón aceptar debe capturar los datos ingresados en el input y transformarlos en un object de tipo pokemon
            //Iniciar el proceso de conexión con la base de datos
            //Cargar esos datos en la base de datos

            //primero creamos el objeto
            Pokemon poke = new Pokemon();
            PokemonNegocio negocio = new PokemonNegocio();  //instanciamos el objeto que hace la conexión a la base de datos


            try
            {
                poke.Numero = int.Parse(txtNumero.Text);
                poke.Nombre = txtNombre.Text;
                poke.Descripcion = txtDescripcion.Text;
                poke.UrlImagen = txtUrlImg.Text;
                poke.Tipo = (Elemento)cbxTipo.SelectedItem;
                poke.Debilidad = (Elemento)cbxDebilidad.SelectedItem;

                negocio.agregar(poke);
                MessageBox.Show("Agregado exitosamente");

                Close();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString()); //esto es para lanzar un mensaje con el error
            }
        }

        private void frmAtlaPokemon_Load(object sender, EventArgs e)
        {
            ElementoNegocio elementnegocio = new ElementoNegocio();

            cbxTipo.DataSource = elementnegocio.listar();
            cbxDebilidad.DataSource = elementnegocio.listar();


        }
    }
}
