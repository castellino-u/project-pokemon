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
            
            
            cargarImagen("https://static.wikia.nocookie.net/espokemon/images/0/02/Pok%C3%A9_Ball_%28Ilustraci%C3%B3n%29.png/revision/latest?cb=20090125150654");

        }

        private void txtUrlImg_Leave(object sender, EventArgs e)
        {
            
            
            cargarImagen(txtUrlImg.Text);

        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxImg.Load(imagen);


            }
            catch (Exception)
            {
                //vamos a hacer que si la imagen no está, para que no me lance la exepción, pongamos otra cosa, en este caso, una
                //imagen de internet
                pbxImg.Load("https://media.istockphoto.com/id/1222357475/vector/image-preview-icon-picture-placeholder-for-website-or-ui-ux-design-vector-illustration.jpg?s=612x612&w=0&k=20&c=KuCo-dRBYV7nz2gbk4J9w1WtTAgpTdznHu55W9FjimE=");
            }
        }
    }
}
