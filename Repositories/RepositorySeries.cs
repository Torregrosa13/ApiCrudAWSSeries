using ApiCrudAWSSeries.Data;
using ApiCrudAWSSeries.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCrudAWSSeries.Repositories
{
    public class RepositorySeries
    {
        private SeriesContext context;

        public RepositorySeries(SeriesContext context) 
        {
            this.context = context;
        }

        public async Task<List<Serie>> GetSeriesAsync() 
        {
            return await this.context.Series.ToListAsync();
        }

        public async Task<Serie> FindSerieAsync(int idSerie) {
            return await this.context.Series.FindAsync(idSerie);
        }

        public async Task<int> GetMaxIdSerieAsync() {
            return await this.context.Series.MaxAsync(s => s.IdSerie) + 1;
        }

        public async Task CreateSerieAsync(string nombre, string imagen, int anyo) {
            Serie serie = new Serie();
            serie.IdSerie = await this.GetMaxIdSerieAsync();
            serie.Nombre = nombre;
            serie.Imagen = imagen;
            serie.Anyo = anyo;
            await this.context.Series.AddAsync(serie);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateSerieAsync(int idSerie, string nombre, string imagen, int anyo) {
            Serie serie = await this.FindSerieAsync(idSerie);
            if (serie != null) {
                serie.Nombre = nombre;
                serie.Imagen = imagen;
                serie.Anyo = anyo;
                await this.context.SaveChangesAsync();
            }
        }

        public async Task DeleteSerieAsync(int idSerie) {
            Serie serie = await this.FindSerieAsync(idSerie);
            if (serie != null) {
                this.context.Series.Remove(serie);
                await this.context.SaveChangesAsync();
            }
        }
    }
}
