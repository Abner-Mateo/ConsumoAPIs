using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace PokemonApp
{
    public partial class Form1 : Form
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public Form1()
        {
            InitializeComponent();
        }


        private void lblNombre_Click(object sender, EventArgs e)
        {


        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            string pokemonName = txtBusqueda.Text.ToLower().Trim();
            if (string.IsNullOrEmpty(pokemonName))
            {
                MessageBox.Show("Por favor, ingrese un nombre de Pokémon.");
                return;
            }

            await ObtenerDatosPokemonAsync(pokemonName);
        }

        private async Task ObtenerDatosPokemonAsync(string nombre)
        {
            try
            {
                string url = $"https://pokeapi.co/api/v2/pokemon/{nombre}";
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("No se encontró el Pokémon o hubo un error.");
                    return;
                }

                string json = await response.Content.ReadAsStringAsync();
                JObject datos = JObject.Parse(json);

                lblNombre.Text = $"Nombre: {datos["name"]}";
                lblPeso.Text = $"Peso: {datos["weight"]} kg";
                lblAltura.Text = $"Altura: {datos["height"]} m";

                picImagen.ImageLocation = datos["sprites"]["front_default"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener datos: {ex.Message}");
            }

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBusqueda.Clear(); // Limpiar el campo de búsqueda
            lblNombre.Text = "Nombre: ";
            lblPeso.Text = "Peso: ";
            lblAltura.Text = "Altura: ";
            picImagen.ImageLocation = null; // Eliminar la imagen
        }



        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }

        private void picPokeball_Click(object sender, EventArgs e)
        {

        }
    }

}


