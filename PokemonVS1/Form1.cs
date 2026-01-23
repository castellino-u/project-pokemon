using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            ocultarColumnas();
            //Acá cargamos los datos de un desplegable
            cboCampo.Items.Add("Número");
            cboCampo.Items.Add("Nombre");
            cboCampo.Items.Add("Descripción");


        }

        //Método para cargar datos en la grilla 
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

                ocultarColumnas();
                cargarImagen(listaPokemon[0].UrlImagen);
                //dgvPokemons.Columns["Id"].Visible = false;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }

        private void ocultarColumnas()
        {
            dgvPokemons.Columns["UrlImagen"].Visible = false; //hacemos esto para no mostrar una columna y ver solo lo que queremos ver. Solo ocultamos una columna
            dgvPokemons.Columns["Id"].Visible = false;
            dgvPokemons.Columns["Estado"].Visible = false; 
        }

        private void dgvPokemons_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvPokemons.CurrentRow != null)
            {
                Pokemon seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.UrlImagen);

            }           
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
            try
            {
                Pokemon seleccionado;
                seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
                frmAtlaPokemon modificar = new frmAtlaPokemon(seleccionado);
                modificar.ShowDialog();  //el showdialog es para que no me permita volver a la vista anterior hasta que termine de trabajar ahí

            }
            catch (Exception ex)
            {
                // MessageBox.Show("Error al modificar pokemon.\n" + ex.Message, "Error",MessageBoxButtons.OK,
                //MessageBoxIcon.Error);
                MessageBox.Show(ex.ToString());
            }
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


        private bool validar()
        {
            if (txtFiltroAvanzado.Text == "")
            {
                MessageBox.Show("Campo de filtro vacío");
                return true;
            }

            if(cboCampo.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, seleccione un campo");
                return true;
            }

            if (cboCriterio.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, seleccione un criterio de busqueda");
                return true;
            }

            if(cboCampo.SelectedItem.ToString() == "Número")
            {
                int numero;
                if (!(int.TryParse(txtFiltroAvanzado.Text, out numero)))
                {
                    MessageBox.Show("Debe ingresar un número");
                    return true;
                }
            }

            return false;
        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {
            PokemonNegocio negocio = new PokemonNegocio();
            try
            {
                if (validar())
                {
                    return;
                }
                string filtro = txtFiltroAvanzado.Text;
                string campo = cboCampo.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();
                
                
                if(cboCampo.SelectedItem != null && cboCriterio.SelectedItem != null){
                    dgvPokemons.DataSource = null;
                    dgvPokemons.DataSource = negocio.filtrar(campo, criterio, filtro);
                    ocultarColumnas();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Pokemon> listaFiltrada;


            string filtrado = txtFiltroRapido.Text.Trim();

            int numero;

            if (int.TryParse(filtrado, out numero))
            {

                listaFiltrada = listaPokemon.FindAll(x => x.Numero == numero);

            }
            else if (filtrado.Length >= 2)
            {
                filtrado = Regex.Replace(filtrado, @"\s+", " ");
                //listaFiltrada = listaPokemon.FindAll(x => x.Nombre.ToLower() == filtrado.ToLower()); esta es la versión anteior, Hay una mejorada
                listaFiltrada = listaPokemon.FindAll(x => x.Nombre.ToLower().Contains(filtrado.ToLower()) || x.Tipo.Descripcion.ToLower().Contains(filtrado.ToLower()));
            }
            else
            {
                listaFiltrada = listaPokemon;
            }

            dgvPokemons.DataSource = null;
            dgvPokemons.DataSource = listaFiltrada;
            ocultarColumnas();

        }

        

        private void cboCampo_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string opcion = cboCampo.SelectedItem.ToString();
            if (opcion == "Número")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Mayor a");
                cboCriterio.Items.Add("Menor a");
                cboCriterio.Items.Add("Igual a");

            }
            else
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Empieza con");
                cboCriterio.Items.Add("Termina con");
                cboCriterio.Items.Add("Contiene");

            }
        }

        private void btnReiniciarBusqueda_Click(object sender, EventArgs e)
        {
            cargarDatos();
        }
    }
}
