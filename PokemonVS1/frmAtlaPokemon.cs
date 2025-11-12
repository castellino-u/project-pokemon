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
        private Pokemon pokemon = null;
        public frmAtlaPokemon()
        {
            InitializeComponent();
            Text = "Agregar Pokemon";
        }

        public frmAtlaPokemon(Pokemon poke)
        {
            InitializeComponent();
            this.pokemon = poke;
            Text = "Modificar Pokemon";

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
            //Pokemon poke = new Pokemon();
            PokemonNegocio negocio = new PokemonNegocio();  //instanciamos el objeto que hace la conexión a la base de datos


            try
            {
                if (pokemon == null) { pokemon = new Pokemon(); }   
                pokemon.Numero = int.Parse(txtNumero.Text);
                pokemon.Nombre = txtNombre.Text;
                pokemon.Descripcion = txtDescripcion.Text;
                pokemon.UrlImagen = txtUrlImg.Text;
                pokemon.Tipo = (Elemento)cbxTipo.SelectedItem;
                pokemon.Debilidad = (Elemento)cbxDebilidad.SelectedItem;

                if(pokemon.Id != 0)
                {
                    negocio.modificar(pokemon);
                    MessageBox.Show("Modificado exitosamente");
                }  
                else
                {
                    negocio.agregar(pokemon);
                    MessageBox.Show("Agregado exitosamente");
                }

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

            try
            {
                cbxTipo.DataSource = elementnegocio.listar();
                //vamos a enlazar los elementos 
                cbxTipo.ValueMember = "Id";
                cbxTipo.DisplayMember = "Descripcion";
                cbxDebilidad.DataSource = elementnegocio.listar();
                cbxDebilidad.ValueMember = "Id";
                cbxDebilidad.DisplayMember = "Descripcion";

                cargarImagen("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS288PhAcjBX73e6M7EgZDylm5-vss4koPpxQ&s");



                if (pokemon != null)
                {
                    //si viene con un pokemon, entonces cargamos todos los txt con los datos de ese pokemon
                    txtNumero.Text = pokemon.Numero.ToString();
                    txtNombre.Text = pokemon.Nombre;
                    txtDescripcion.Text = pokemon.Descripcion;
                    txtUrlImg.Text = pokemon.UrlImagen;

                    //cargamos la imagen que trae el object pokemon
                    cargarImagen(pokemon.UrlImagen);
                    
                    //preseleccionamos valores
                    cbxTipo.SelectedValue = pokemon.Tipo.Id;
                    cbxDebilidad.SelectedValue = pokemon.Debilidad.Id;


                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
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
