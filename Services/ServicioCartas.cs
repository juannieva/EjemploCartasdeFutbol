using DAO;
using DAO.Interface;
using Ninject;
using Services.Modelos;
using Servicios.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Servicios
{
    public class ServicioCartas
    {
        private ICartasDAO _cartasDAO;
        private IColeccionDAO _coleccionDAO;
        private IJugadoresDAO _jugadoresDAO;
        private ISeriesDAO _seriesDAO;
        private IRarezaDAO _rarezasDAO;
        private StandardKernel kernel;


        public ServicioCartas()
        {
            this.kernel = LoadNinject(kernel);
            this._cartasDAO = kernel.Get<CartasDAO>();
            this._coleccionDAO = kernel.Get<ColeccionDAO>();
            this._jugadoresDAO = kernel.Get<JugadoresDAO>();
            this._seriesDAO = kernel.Get<SeriesDAO>();
            this._rarezasDAO = kernel.Get<RarezasDAO>();
        }

        private StandardKernel LoadNinject(StandardKernel kernel)
        {
            kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            return kernel;
        }

        public void InsertCarta(InsertCartaModel model)
        {
            if (model.idMazo == 0)
            {
                if (model.colecciones == null)
                    {
                        _cartasDAO.Insert(model.idJugador, model.idRareza, model.idSerie);
                }else
                {
                    _cartasDAO.Insert(model.idJugador, model.idRareza, model.idSerie, model.colecciones);
                }
            }

            else if(model.idMazo >0)
            {
                if (model.colecciones.Count() == 0 || model.colecciones == null)
                {
                    _cartasDAO.Insert(model.idJugador, model.idMazo, model.idRareza, model.idSerie);
                }else
                {
                    _cartasDAO.Insert(model.idJugador, model.idMazo, model.idRareza, model.idSerie, model.colecciones);
                }
            }
            
        }

        public void DeleteLogico(int id)
        {
            _cartasDAO.DeleteLogico(id);
        }

        public void Update(int idCarta,InsertCartaModel model)
        {
            if (model.idMazo == 0)
            {
                if ( model.colecciones == null)
                {
                    _cartasDAO.Update(idCarta, model.idRareza, model.idSerie);
                    _coleccionDAO.DeleteAllCartaINColeccion(idCarta);
                }
                else
                {
                    _cartasDAO.Update(idCarta, model.idRareza, model.idSerie, ComprobarColecciones(idCarta, model.colecciones));
                }
            }

            else if (model.idMazo > 0)
            {
                if ( model.colecciones == null)
                {
                    _cartasDAO.Update(idCarta, model.idMazo, model.idRareza, model.idSerie);
                    _coleccionDAO.DeleteAllCartaINColeccion(idCarta);
                }
                else
                {
                    _cartasDAO.Update(idCarta, model.idMazo, model.idRareza, model.idSerie, ComprobarColecciones(idCarta, model.colecciones));
                }
            }
        }

        private List<int> ComprobarColecciones(int idCarta,List<int> colecciones)
        {
            List<int> auxiliar = new List<int>(colecciones);
            foreach (var i in _coleccionDAO.GetColeccionesINCarta(idCarta))
            {
                if (!auxiliar.Contains(i))
                {
                    _coleccionDAO.DeleteCartaINColeccion(idCarta, i);
                }
            }
            foreach (var i in colecciones)
            {
                if (_coleccionDAO.GetColeccionesINCarta(idCarta).Contains(i))
                {
                    auxiliar.Remove(i);
                }
            }
            
            return auxiliar;
        }

        public List<CartaModel> BuscarCartas()
        {
            List<CartaModel> cartasModel=new List<CartaModel>();
            foreach (var i in _cartasDAO.GetAll().ToList())
            {
                CartaModel modelo= new CartaModel(i.idCarta, i.idSerie, i.nombre, i.apellido, i.nombreEquipo, i.descripcionNacionalidad, i.descripcionRolJugador, i.color, i.descripcionRareza,i.urlImagen,i.active);
                //Este forEach busca en que coleccion esta la carta y la agrega al modelo para poder mostrar la coleccion en la vista
                foreach(var j in _coleccionDAO.GetAllCartasINColeccion().ToList())
                {
                    if (i.idCarta == j.idCarta)
                    {
                        modelo.colecciones.Add(new ColeccionModel(j.idColeccion, _coleccionDAO.GetId(j.idColeccion).descripcionColeccion));
                    }
                }
                cartasModel.Add(modelo);
            }
            return cartasModel;
        }

        public CartaModel BuscarCartas(int id)
        {
            CartaModel cartaModel = new CartaModel(_cartasDAO.GetId(id).idCarta, _cartasDAO.GetId(id).idSerie, _cartasDAO.GetId(id).nombre, _cartasDAO.GetId(id).apellido, _cartasDAO.GetId(id).nombreEquipo, _cartasDAO.GetId(id).descripcionNacionalidad, _cartasDAO.GetId(id).descripcionRolJugador, _cartasDAO.GetId(id).color, _cartasDAO.GetId(id).descripcionRareza, _cartasDAO.GetId(id).urlImagen,_cartasDAO.GetId(id).active);
            //Este forEach busca en que coleccion esta la carta y la agrega al modelo para poder mostrar la coleccion en la vista
            foreach (var j in _coleccionDAO.GetAllCartasINColeccion().ToList())
            {
                if (cartaModel.id == j.idCarta)
                {
                    cartaModel.colecciones.Add(new ColeccionModel(j.idColeccion, _coleccionDAO.GetId(j.idColeccion).descripcionColeccion));
                }
            }
            return cartaModel;
        }
       
        public ListaOpciones ConvertirListasASelectListItem()
        {
            List<SelectListItem> colecciones = new List<SelectListItem>();
            List<SelectListItem> jugadores = new List<SelectListItem>();
            List<SelectListItem> series = new List<SelectListItem>();
            List<SelectListItem> rarezas = new List<SelectListItem>();
            ListaOpciones listaOpciones = new ListaOpciones();
            foreach (var i in _coleccionDAO.GetAll().ToList())
            {
                colecciones.Add(new SelectListItem()
                {
                    Text = i.descripcionColeccion,
                    Value = i.idColeccion.ToString()
                });
            }
            foreach (var i in _jugadoresDAO.GetAll().ToList())
            {
                jugadores.Add(new SelectListItem()
                {
                    Text = i.nombre+" "+i.apellido,
                    Value = i.idJugador.ToString()
                });
            }
            foreach (var i in _seriesDAO.GetAll().ToList())
            {
                series.Add(new SelectListItem()
                {
                    Text = i.numSerie.ToString(),
                    Value = i.idSerie.ToString()
                }) ;
            }
            foreach (var i in _rarezasDAO.GetAll().ToList())
            {
                rarezas.Add(new SelectListItem()
                {
                    Text = i.descripcionRareza.ToString(),
                    Value = i.idRareza.ToString()
                });
            }
            listaOpciones.colecciones = colecciones;
            listaOpciones.jugadores = jugadores;
            listaOpciones.series = series;
            listaOpciones.rarezas = rarezas;
            return listaOpciones;
        }

        public ListaOpciones ConvertirListasASelectListItem(int id)
        {
            List<SelectListItem> colecciones = new List<SelectListItem>();
            List<SelectListItem> jugadores = new List<SelectListItem>();
            List<SelectListItem> series = new List<SelectListItem>();
            List<SelectListItem> rarezas = new List<SelectListItem>();
            ListaOpciones listaOpciones= new ListaOpciones();

            foreach (var i in _coleccionDAO.GetAll().ToList())
            {
                        colecciones.Add(new SelectListItem()
                        {
                            Text = i.descripcionColeccion,

                            Value = i.idColeccion.ToString()
                        });
            }
            foreach (var i in _coleccionDAO.GetColeccionesINCarta(id))
            {
                foreach (var j in colecciones)
                {
                    if (j.Value == i.ToString())
                    {
                        j.Selected = true;
                    }
                }
            }

            var auxJugador = _cartasDAO.GetJugadorEnCarta(id);
            jugadores.Add(new SelectListItem()
                {
                    Text = auxJugador.nombre + " " + auxJugador.apellido,
                    Value = auxJugador.idJugador.ToString(),
                    Selected = true
            });

            var auxSerie = _seriesDAO.GetId(_cartasDAO.GetId(id).idSerie);
            series.Add(new SelectListItem()
                {
                    Text = auxSerie.numSerie.ToString(),
                    Value = auxSerie.idSerie.ToString(),
                    Selected=true
                }
            );
            
            var auxRareza = _cartasDAO.GetId(id).descripcionRareza;
            foreach (var i in _rarezasDAO.GetAll().ToList())
            {
                if (auxRareza==i.descripcionRareza)
                {
                    rarezas.Add(new SelectListItem()
                    {
                        Text = i.descripcionRareza,
                        Value = i.idRareza.ToString(),
                        Selected = true
                    });
                }else if(auxRareza != i.descripcionRareza)
                {
                    rarezas.Add(new SelectListItem()
                    {
                        Text = i.descripcionRareza,
                        Value = i.idRareza.ToString()
                    });
                }
                
            }
            listaOpciones.colecciones = colecciones;
            listaOpciones.jugadores = jugadores;
            listaOpciones.series = series;
            listaOpciones.rarezas = rarezas;
            return listaOpciones;
        }

        public List<SelectListItem> JugadoresEnSerie(int serie)
        {
            List<SelectListItem> jugadores = new List<SelectListItem>();
            foreach (var i in _jugadoresDAO.GetAllInSerie(serie))
            {
                jugadores.Add(new SelectListItem()
                {
                    Text = i.nombre + " " + i.apellido,
                    Value = i.idJugador.ToString()
                });
            }
            return jugadores;
        }

        //SOLUCION A ERROR AL INSERTAR UN PARAMETRO NO VALIDO EN LA VISTA
        public List<SelectListItem> JugadoresEnSerieError(int serie)
        {
            List<SelectListItem> jugadores = new List<SelectListItem>();
            foreach (var i in _jugadoresDAO.GetAllInSerie(serie))
            {
                jugadores.Add(new SelectListItem()
                {
                    Text = i.nombre + " " + i.apellido,
                    Value = i.idJugador.ToString()
                });
            }
            return jugadores;
        }
        

    }
}
