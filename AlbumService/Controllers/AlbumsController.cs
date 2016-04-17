using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AlbumService.Models;

namespace AlbumService.Controllers
{
    public class AlbumsController : ApiController
    {
        private AlbumServiceContext db = new AlbumServiceContext();

        // GET: api/Albums
        //public IQueryable<Album> GetAlbums()
        //{
        //    return db.Albums.Include(b => b.Artist);

        //    // Eager loading (optional) which references the Artist instead of null on the GET request
        //    //.Include(b => b.Artist);
        //}

        public IQueryable<AlbumDTO> GetAlbums()
        {
            var albums = from a in db.Albums
                         select new AlbumDTO()
                         {
                             Id = a.Id,
                             Title = a.Title,
                             ArtistName = a.Artist.Name
                         };

            return albums;
        }

        [ResponseType(typeof(AlbumDetailDTO))]
        public async Task<IHttpActionResult> GetAlbum(int id)
        {
            var album = await db.Albums.Include(a => a.Artist).Select(a =>
            new AlbumDetailDTO()
            {
                Id = a.Id,
                Title = a.Title,
                Year = a.Year,
                Price = a.Price,
                ArtistName = a.Artist.Name,
                Genre = a.Genre
            }).SingleOrDefaultAsync(a => a.Id == id);

            if (album == null)
            {
                return NotFound();
            }

            return Ok(album);
        }



        // GET: api/Albums/5
        //[ResponseType(typeof(Album))]
        //public async Task<IHttpActionResult> GetAlbum(int id)
        //{
        //    Album album = await db.Albums.FindAsync(id);
        //    if (album == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(album);
        //}

        // PUT: api/Albums/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAlbum(int id, Album album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != album.Id)
            {
                return BadRequest();
            }

            db.Entry(album).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbumExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Albums
        //[ResponseType(typeof(Album))]
        //public async Task<IHttpActionResult> PostAlbum(Album album)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Albums.Add(album);
        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("DefaultApi", new { id = album.Id }, album);
        //}

       // POST: api/Albums
       [ResponseType(typeof(Album))]
        public async Task<IHttpActionResult> PostAlbum(Album album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Albums.Add(album);
            await db.SaveChangesAsync();

            db.Entry(album).Reference(x => x.Artist).Load();

            var dto = new AlbumDTO()
            {
                Id = album.Id,
                Title = album.Title,
                ArtistName = album.Artist.Name
            };

            return CreatedAtRoute("DefaultApi", new { id = album.Id }, album);
        }




        // DELETE: api/Albums/5
        [ResponseType(typeof(Album))]
        public async Task<IHttpActionResult> DeleteAlbum(int id)
        {
            Album album = await db.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            db.Albums.Remove(album);
            await db.SaveChangesAsync();

            return Ok(album);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlbumExists(int id)
        {
            return db.Albums.Count(e => e.Id == id) > 0;
        }
    }
}