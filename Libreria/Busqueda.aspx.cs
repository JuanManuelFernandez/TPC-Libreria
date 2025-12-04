using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Libreria
{
    public partial class Busqueda : System.Web.UI.Page
    {
        private AccesoDatos datos = null;

        // ViewState para mantener filtros seleccionados
        private List<int> GenerosSeleccionados
        {
            get { return ViewState["GenerosSeleccionados"] as List<int> ?? new List<int>(); }
            set { ViewState["GenerosSeleccionados"] = value; }
        }

        private List<int> EditorialesSeleccionadas
        {
            get { return ViewState["EditorialesSeleccionadas"] as List<int> ?? new List<int>(); }
            set { ViewState["EditorialesSeleccionadas"] = value; }
        }

        private List<int> AutoresSeleccionados
        {
            get { return ViewState["AutoresSeleccionados"] as List<int> ?? new List<int>(); }
            set { ViewState["AutoresSeleccionados"] = value; }
        }

        private string TerminoBusqueda
        {
            get { return ViewState["TerminoBusqueda"] as string ?? string.Empty; }
            set { ViewState["TerminoBusqueda"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string termino = Request.QueryString["q"];
                string generoId = Request.QueryString["genero"];

                // CASO 1: Filtro por género desde MasterPage
                if (!string.IsNullOrEmpty(generoId))
                {
                    int idGenero = int.Parse(generoId);
                    GenerosSeleccionados = new List<int> { idGenero };

                    AccesoGeneros negocioGeneros = new AccesoGeneros();
                    Genero genero = negocioGeneros.BuscarPorIdGenero(idGenero);
                    if (genero != null)
                    {
                        litTermino.Text = genero.Nombre;
                        TerminoBusqueda = ""; // No hay término de búsqueda, solo filtro
                    }

                    CargarFiltros();
                    AplicarFiltros();
                }
                // CASO 2: Búsqueda por término
                else if (!string.IsNullOrEmpty(termino))
                {
                    litTermino.Text = Server.HtmlEncode(termino);
                    TerminoBusqueda = termino;
                    CargarFiltros();
                    AplicarFiltros();
                }
                // CASO 3: Sin parámetros, redirigir
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
            else
            {
                // Manejar la eliminación de filtros individuales desde los tags
                string eventTarget = Request.Form["__EVENTTARGET"];
                if (!string.IsNullOrEmpty(eventTarget) && eventTarget.StartsWith("removerFiltro_"))
                {
                    string[] partes = eventTarget.Split('_');
                    if (partes.Length == 3)
                    {
                        string tipoFiltro = partes[1]; // "genero", "editorial", "autor"
                        int idFiltro = int.Parse(partes[2]);

                        RemoverFiltroIndividual(tipoFiltro, idFiltro);
                    }
                }
            }
        }

        private void RemoverFiltroIndividual(string tipo, int id)
        {
            switch (tipo)
            {
                case "genero":
                    GenerosSeleccionados.Remove(id);
                    break;
                case "editorial":
                    EditorialesSeleccionadas.Remove(id);
                    break;
                case "autor":
                    AutoresSeleccionados.Remove(id);
                    break;
            }

            // Recargar filtros y aplicar cambios
            CargarFiltros();
            AplicarFiltros();
        }

        private void CargarFiltros()
        {
            try
            {
                AccesoLibros negocioLibros = new AccesoLibros();
                List<Libro> todosLibros = negocioLibros.Listar();

                // Si hay un término de búsqueda, filtrar primero por ese término
                // para que los contadores de los filtros reflejen solo los libros que coinciden
                if (!string.IsNullOrEmpty(TerminoBusqueda))
                {
                    todosLibros = negocioLibros.BuscarLibros(TerminoBusqueda);
                }

                // Cargar filtro de géneros con conteo
                var generosConConteo = todosLibros
                    .GroupBy(l => l.IdGenero)
                    .Select(g => new
                    {
                        IdGenero = g.Key,
                        Nombre = ObtenerNombreGenero(g.Key),
                        Cantidad = g.Count()
                    })
                    .OrderBy(x => x.Nombre)
                    .ToList();

                rptFiltroGeneros.DataSource = generosConConteo;
                rptFiltroGeneros.DataBind();

                // Cargar filtro de editoriales con conteo
                var editorialesConConteo = todosLibros
                    .GroupBy(l => l.IdEditorial)
                    .Select(e => new
                    {
                        IdEditorial = e.Key,
                        Nombre = ObtenerNombreEditorial(e.Key),
                        Cantidad = e.Count()
                    })
                    .OrderBy(x => x.Nombre)
                    .ToList();

                rptFiltroEditoriales.DataSource = editorialesConConteo;
                rptFiltroEditoriales.DataBind();

                // Cargar filtro de autores con conteo
                var autoresConConteo = todosLibros
                    .GroupBy(l => l.IdAutor)
                    .Select(a => new
                    {
                        IdAutor = a.Key,
                        NombreCompleto = ObtenerNombreAutor(a.Key),
                        Cantidad = a.Count()
                    })
                    .OrderBy(x => x.NombreCompleto)
                    .ToList();

                rptFiltroAutores.DataSource = autoresConConteo;
                rptFiltroAutores.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar filtros: " + ex.Message;
                lblMensaje.CssClass = "alert alert-danger";
                lblMensaje.Visible = true;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            // Marcar los checkboxes justo antes de renderizar
            MarcarFiltrosSeleccionados();

            // Registrar el script para manejar los clicks en los tags
            RegistrarScriptRemoverFiltros();
        }

        private void RegistrarScriptRemoverFiltros()
        {
            string script = @"
                function removerFiltro(tipo, id) {
                    __doPostBack('removerFiltro_' + tipo + '_' + id, '');
                }
            ";

            ScriptManager.RegisterStartupScript(this, GetType(), "RemoverFiltroScript", script, true);
        }

        private void MarcarFiltrosSeleccionados()
        {
            // Marcar géneros seleccionados
            foreach (RepeaterItem item in rptFiltroGeneros.Items)
            {
                CheckBox chk = (CheckBox)item.FindControl("chkGenero");
                HiddenField hdn = (HiddenField)item.FindControl("hdnGeneroId");
                if (chk != null && hdn != null)
                {
                    int id = int.Parse(hdn.Value);
                    chk.Checked = GenerosSeleccionados.Contains(id);
                }
            }

            // Marcar editoriales seleccionadas
            foreach (RepeaterItem item in rptFiltroEditoriales.Items)
            {
                CheckBox chk = (CheckBox)item.FindControl("chkEditorial");
                HiddenField hdn = (HiddenField)item.FindControl("hdnEditorialId");
                if (chk != null && hdn != null)
                {
                    int id = int.Parse(hdn.Value);
                    chk.Checked = EditorialesSeleccionadas.Contains(id);
                }
            }

            // Marcar autores seleccionados
            foreach (RepeaterItem item in rptFiltroAutores.Items)
            {
                CheckBox chk = (CheckBox)item.FindControl("chkAutor");
                HiddenField hdn = (HiddenField)item.FindControl("hdnAutorId");
                if (chk != null && hdn != null)
                {
                    int id = int.Parse(hdn.Value);
                    chk.Checked = AutoresSeleccionados.Contains(id);
                }
            }
        }

        protected void FiltroChanged(object sender, EventArgs e)
        {
            // PRIMERO: Actualizar las listas de filtros seleccionados
            ActualizarFiltrosSeleccionados();

            // SEGUNDO: Recargar los filtros (para que se muestren correctamente)
            CargarFiltros();

            // TERCERO: Aplicar los filtros a los resultados
            AplicarFiltros();

            // CUARTO: Mostrar los filtros activos
            MostrarFiltrosActivos();
        }

        private void ActualizarFiltrosSeleccionados()
        {
            // Limpiar las listas
            List<int> nuevosGeneros = new List<int>();
            List<int> nuevasEditoriales = new List<int>();
            List<int> nuevosAutores = new List<int>();

            // Actualizar géneros
            foreach (RepeaterItem item in rptFiltroGeneros.Items)
            {
                CheckBox chk = (CheckBox)item.FindControl("chkGenero");
                HiddenField hdn = (HiddenField)item.FindControl("hdnGeneroId");
                if (chk != null && hdn != null && chk.Checked)
                {
                    nuevosGeneros.Add(int.Parse(hdn.Value));
                }
            }

            // Actualizar editoriales
            foreach (RepeaterItem item in rptFiltroEditoriales.Items)
            {
                CheckBox chk = (CheckBox)item.FindControl("chkEditorial");
                HiddenField hdn = (HiddenField)item.FindControl("hdnEditorialId");
                if (chk != null && hdn != null && chk.Checked)
                {
                    nuevasEditoriales.Add(int.Parse(hdn.Value));
                }
            }

            // Actualizar autores
            foreach (RepeaterItem item in rptFiltroAutores.Items)
            {
                CheckBox chk = (CheckBox)item.FindControl("chkAutor");
                HiddenField hdn = (HiddenField)item.FindControl("hdnAutorId");
                if (chk != null && hdn != null && chk.Checked)
                {
                    nuevosAutores.Add(int.Parse(hdn.Value));
                }
            }

            // Guardar en ViewState
            GenerosSeleccionados = nuevosGeneros;
            EditorialesSeleccionadas = nuevasEditoriales;
            AutoresSeleccionados = nuevosAutores;
        }

        private void AplicarFiltros()
        {
            try
            {
                AccesoLibros negocioLibros = new AccesoLibros();
                AccesoAutores negocioAutores = new AccesoAutores();
                List<Libro> resultados;

                // PASO 1: Obtener lista base de libros
                // Si hay un término de búsqueda, filtrar por ese término primero
                if (!string.IsNullOrEmpty(TerminoBusqueda))
                {
                    resultados = negocioLibros.BuscarLibros(TerminoBusqueda);
                }
                else
                {
                    // Si no hay término, obtener todos los libros
                    resultados = negocioLibros.Listar();
                }

                // PASO 2: Aplicar filtro de géneros
                if (GenerosSeleccionados != null && GenerosSeleccionados.Count > 0)
                {
                    resultados = resultados.Where(l => GenerosSeleccionados.Contains(l.IdGenero)).ToList();
                }

                // PASO 3: Aplicar filtro de editoriales
                if (EditorialesSeleccionadas != null && EditorialesSeleccionadas.Count > 0)
                {
                    resultados = resultados.Where(l => EditorialesSeleccionadas.Contains(l.IdEditorial)).ToList();
                }

                // PASO 4: Aplicar filtro de autores
                if (AutoresSeleccionados != null && AutoresSeleccionados.Count > 0)
                {
                    resultados = resultados.Where(l => AutoresSeleccionados.Contains(l.IdAutor)).ToList();
                }

                // PASO 5: Mostrar resultados
                if (resultados.Count > 0)
                {
                    var librosMostrar = new List<object>();

                    foreach (var libro in resultados)
                    {
                        librosMostrar.Add(new
                        {
                            libro.IdLibro,
                            libro.Titulo,
                            libro.Descripcion,
                            NombreAutor = negocioAutores.ObtenerNombreCompleto(libro.IdAutor),
                            libro.Precio
                        });
                    }

                    rptLibros.DataSource = librosMostrar;
                    rptLibros.DataBind();

                    rptLibros.Visible = true;
                    pnlNoLibros.Visible = false;
                    lblMensaje.Visible = false;

                    // Mostrar cantidad de resultados
                    litCantidadResultados.Text = $"Se encontraron {resultados.Count} resultado(s)";
                }
                else
                {
                    rptLibros.Visible = false;
                    pnlNoLibros.Visible = true;
                    litCantidadResultados.Text = "0 resultados";
                }

                // Mostrar filtros activos
                MostrarFiltrosActivos();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al aplicar filtros: " + ex.Message;
                lblMensaje.CssClass = "alert alert-danger";
                lblMensaje.Visible = true;
            }
        }

        private void MostrarFiltrosActivos()
        {
            bool hayFiltros = (GenerosSeleccionados != null && GenerosSeleccionados.Count > 0) ||
                             (EditorialesSeleccionadas != null && EditorialesSeleccionadas.Count > 0) ||
                             (AutoresSeleccionados != null && AutoresSeleccionados.Count > 0);

            if (hayFiltros)
            {
                string filtrosHtml = "";

                // Mostrar géneros seleccionados
                if (GenerosSeleccionados != null)
                {
                    foreach (int id in GenerosSeleccionados)
                    {
                        string nombreGenero = ObtenerNombreGenero(id);
                        filtrosHtml += $"<span class='filter-tag'>{nombreGenero} " +
                                      $"<span class='remove-filter' onclick='removerFiltro(\"genero\", {id})' style='cursor: pointer;'>×</span></span>";
                    }
                }

                // Mostrar editoriales seleccionadas
                if (EditorialesSeleccionadas != null)
                {
                    foreach (int id in EditorialesSeleccionadas)
                    {
                        string nombreEditorial = ObtenerNombreEditorial(id);
                        filtrosHtml += $"<span class='filter-tag'>{nombreEditorial} " +
                                      $"<span class='remove-filter' onclick='removerFiltro(\"editorial\", {id})' style='cursor: pointer;'>×</span></span>";
                    }
                }

                // Mostrar autores seleccionados
                if (AutoresSeleccionados != null)
                {
                    foreach (int id in AutoresSeleccionados)
                    {
                        string nombreAutor = ObtenerNombreAutor(id);
                        filtrosHtml += $"<span class='filter-tag'>{nombreAutor} " +
                                      $"<span class='remove-filter' onclick='removerFiltro(\"autor\", {id})' style='cursor: pointer;'>×</span></span>";
                    }
                }

                litFiltrosActivos.Text = filtrosHtml;
                pnlFiltrosActivos.Visible = true;
            }
            else
            {
                pnlFiltrosActivos.Visible = false;
            }
        }

        protected void BtnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            // Limpiar todos los filtros
            GenerosSeleccionados = new List<int>();
            EditorialesSeleccionadas = new List<int>();
            AutoresSeleccionados = new List<int>();

            // Recargar filtros para desmarcar los checkboxes
            CargarFiltros();

            // Aplicar filtros (sin filtros = mostrar todos los resultados de búsqueda)
            AplicarFiltros();

            // Ocultar panel de filtros activos
            pnlFiltrosActivos.Visible = false;
        }

        private void CargarResultados(string termino)
        {
            try
            {
                AccesoLibros negocioLibros = new AccesoLibros();
                AccesoAutores negocioAutores = new AccesoAutores();

                List<Libro> resultados = negocioLibros.BuscarLibros(termino);

                if (resultados.Count > 0)
                {
                    var librosMostrar = new List<object>();

                    foreach (var libro in resultados)
                    {
                        librosMostrar.Add(new
                        {
                            libro.IdLibro,
                            libro.Titulo,
                            libro.Descripcion,
                            NombreAutor = negocioAutores.ObtenerNombreCompleto(libro.IdAutor),
                            libro.Precio
                        });
                    }

                    rptLibros.DataSource = librosMostrar;
                    rptLibros.DataBind();

                    rptLibros.Visible = true;
                    pnlNoLibros.Visible = false;
                    lblMensaje.Visible = false;

                    litCantidadResultados.Text = $"Se encontraron {resultados.Count} resultado(s)";
                }
                else
                {
                    rptLibros.Visible = false;
                    pnlNoLibros.Visible = true;
                    lblMensaje.Text = "No se encontraron resultados para tu búsqueda. Intenta con otros términos.";
                    lblMensaje.CssClass = "alert alert-warning";
                    lblMensaje.Visible = true;
                    litCantidadResultados.Text = "0 resultados";
                }
            }
            catch (Exception ex)
            {
                rptLibros.Visible = false;
                lblMensaje.Text = "Error al realizar la búsqueda: " + ex.Message;
                lblMensaje.CssClass = "alert alert-danger";
                lblMensaje.Visible = true;
            }
        }

        // Métodos auxiliares para obtener nombres
        private string ObtenerNombreGenero(int idGenero)
        {
            try
            {
                AccesoGeneros negocioGeneros = new AccesoGeneros();
                Genero genero = negocioGeneros.BuscarPorIdGenero(idGenero);
                return genero?.Nombre ?? "Desconocido";
            }
            catch
            {
                return "Desconocido";
            }
        }

        private string ObtenerNombreEditorial(int idEditorial)
        {
            try
            {
                AccesoEditoriales negocioEditoriales = new AccesoEditoriales();
                Editorial editorial = negocioEditoriales.BuscarPorIdEditorial(idEditorial);
                return editorial?.Nombre ?? "Desconocido";
            }
            catch
            {
                return "Desconocido";
            }
        }

        private string ObtenerNombreAutor(int idAutor)
        {
            try
            {
                AccesoAutores negocioAutores = new AccesoAutores();
                return negocioAutores.ObtenerNombreCompleto(idAutor);
            }
            catch
            {
                return "Desconocido";
            }
        }

        // Métodos para carrito y lista de deseados
        protected void Btn_AgregarCarrito(object sender, CommandEventArgs e)
        {
            if (Session["usuario"] != null)
            {
                dynamic usuario = Session["usuario"];
                var dataCli = new AccesoClientes();

                int idCliente = dataCli.Listar().Find(x => x.Usuario.IdUsuario == usuario.IdUsuario).IdCliente;
                int idLibro = Convert.ToInt32(e.CommandArgument);

                try
                {
                    AgregarAlCarrito(idCliente, idLibro);
                    // Mostrar mensaje de éxito o redireccionar
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al agregar al carrito: " + ex.Message;
                    lblMensaje.CssClass = "alert alert-danger";
                    lblMensaje.Visible = true;
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void Btn_AgregarLista(object sender, CommandEventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (usuario != null && usuario.TipoUsuario == TipoUsuario.Cliente)
            {
                var dataCli = new AccesoClientes();
                int idCliente = dataCli.Listar().Find(x => x.Usuario.IdUsuario == usuario.IdUsuario).IdCliente;
                int idLibro = Convert.ToInt32(e.CommandArgument);

                try
                {
                    AgregarLista(idCliente, idLibro);
                    // Mostrar mensaje de éxito
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al agregar a la lista: " + ex.Message;
                    lblMensaje.CssClass = "alert alert-danger";
                    lblMensaje.Visible = true;
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        private void AgregarAlCarrito(int idCliente, int idLibro)
        {
            datos = new AccesoDatos();

            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO Carrito (IDCliente, IDLibro) VALUES (@IDCliente, @IDLibro)");
                datos.SetearParametro("@IDCliente", idCliente);
                datos.SetearParametro("@IDLibro", idLibro);
                datos.EjecutarNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.Cerrar();
            }
        }

        protected void AgregarLista(int idCliente, int idLibro)
        {
            datos = new AccesoDatos();

            try
            {
                datos.Conectar();
                datos.Consultar("INSERT INTO Deseados (IDCliente, IDLibro) VALUES (@IDCliente, @IDLibro)");
                datos.SetearParametro("@IDCliente", idCliente);
                datos.SetearParametro("@IDLibro", idLibro);
                datos.EjecutarNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.Cerrar();
            }
        }
    }
}