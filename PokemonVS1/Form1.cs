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
    public partial class Pokedex : Form
    {
        private List<Pokemon> listaPokemon;
        public Pokedex()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Acá vamos a hacer el evento load para que apenas carge el winform, me cargue la grilla con la lectura que hizo de la db
            cargarDatos();


            
        }

        private void cargarDatos()
        {
            //Acá vamos a hacer el evento load para que apenas carge el winform, me cargue la grilla con la lectura que hizo de la db
            PokemonNegocio negocio = new PokemonNegocio(); //creamos una instancia de mi clase service donde hice toda la lógica 
            try   //aplicamos un trycatch para manejo de errores por si se lee algún dato nulo
            {
                listaPokemon = negocio.Listar();
                dgvPokemons.DataSource = listaPokemon; //a la grilla de datos, le voy a asignar service.listar()
                //serive.listar va a la base de datos y te devuelve una lista de datos, la listapokemon, 
                //que hace dataSource? recibe una lista de datos y lo modela en la tabla 
                dgvPokemons.Columns["UrlImagen"].Visible = false; //hacemos esto para no mostrar una columna y ver solo lo que queremos ver. Solo ocultamos una columna
                cargarImagen(listaPokemon[0].UrlImagen);
                //dgvPokemons.Columns["Id"].Visible = false;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }

        private void dgvPokemons_SelectionChanged(object sender, EventArgs e)
        {
           
            Pokemon seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
            cargarImagen(seleccionado.UrlImagen);
        }


        //acá vamos a hacer una función para capturar la imagen, la vamos a modularizar, porque tranquilamente podríamos ponerla
        //en el mismo método y solo ponemos un try catch y listo, pero vamos a hacerlo modularizado para practicarlo así

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxPokemon.Load(imagen);


            }
            catch (Exception)
            {
                //vamos a hacer que si la imagen no está, para que no me lance la exepción, pongamos otra cosa, en este caso, una
                //imagen de internet
                pbxPokemon.Load("https://media.istockphoto.com/id/1222357475/vector/image-preview-icon-picture-placeholder-for-website-or-ui-ux-design-vector-illustration.jpg?s=612x612&w=0&k=20&c=KuCo-dRBYV7nz2gbk4J9w1WtTAgpTdznHu55W9FjimE=");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //desde este botón llamamos al form de agregar pokemmon
            frmAtlaPokemon alta = new frmAtlaPokemon();
            alta.ShowDialog();  //el showdialog es para que no me permita volver a la vista anterior hasta que termine de trabajar ahí
            cargarDatos();

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //desde este botón llamamos al form de modificar pokemon
            //para modificarlo, primero debemos seleccionar un pokemon
            Pokemon seleccionado;
            seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
            frmAtlaPokemon modificar = new frmAtlaPokemon(seleccionado);
            modificar.ShowDialog();  //el showdialog es para que no me permita volver a la vista anterior hasta que termine de trabajar ahí
            cargarDatos();
        }

        private void btnEliminarFisico_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void btnEliminarLogico_Click(object sender, EventArgs e)
        {
            eliminar(true);
        }

        //vamos a crear un método eliminar
        private void eliminar(bool logico = false)
        {
            PokemonNegocio negocio = new PokemonNegocio();
            Pokemon seleccionado;
            try
            {
                DialogResult resultado = MessageBox.Show("¿Eliminar Elemento seleccionado?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resultado == DialogResult.Yes)
                {
                    seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;

                    if (logico)
                    {
                        negocio.eliminarLogico(seleccionado.Id);
                    }
                    else
                    {
                        negocio.eliminar(seleccionado.Id);
                    }
                    cargarDatos();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
