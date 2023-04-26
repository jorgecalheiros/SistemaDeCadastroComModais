using Microsoft.AspNetCore.Mvc;
using SistemaDeCadastroComModais.Models;
using SistemaDeCadastroComModais.Repositories.Contracts;
using SistemaDeCadastroComModais.ViewModels;

namespace SistemaDeCadastroComModais.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        public IActionResult Index()
        {
            List<ClienteModel> clientes = _clienteRepository.GetAll();
            List<ClienteViewModel> clientesViewModel = new List<ClienteViewModel>();
            clientes.ForEach(cliente =>
            {
                ClienteViewModel clienteViewModel = new ClienteViewModel();
                clienteViewModel.Id = cliente.Id;
                clienteViewModel.Name = cliente.Name;
                clienteViewModel.Telefone = cliente.Telefone;
                clienteViewModel.Email = cliente.Email;
                clientesViewModel.Add(clienteViewModel);
            });
            return View(clientesViewModel);
        }

        public IActionResult Criar()
        {
            ClienteViewModel clienteViewModel = new ClienteViewModel();
            return PartialView("Partials/CriarClienteViewPartial",clienteViewModel);
        }
        
        public IActionResult Editar(int id)
        {
            ClienteModel cliente = _clienteRepository.Get(id);
            if(cliente != null)
            {
                ClienteViewModel clienteViewModel = new ClienteViewModel();
                clienteViewModel.Id= cliente.Id;
                clienteViewModel.Name = cliente.Name;
                clienteViewModel.Email = cliente.Email;
                clienteViewModel.Telefone= cliente.Telefone;
                return PartialView("Partials/EditarClienteViewPartial",clienteViewModel);
            }
            throw new Exception("Error");
        }

        public IActionResult Delete(int id)
        {
            ClienteModel cliente = _clienteRepository.Get(id);
            if (cliente != null)
            {
                ClienteViewModel clienteViewModel = new ClienteViewModel();
                clienteViewModel.Id = cliente.Id;
                clienteViewModel.Name = cliente.Name;
                clienteViewModel.Email = cliente.Email;
                clienteViewModel.Telefone = cliente.Telefone;
                return PartialView("Partials/ConfirmarExcluirClienteViewPartial", clienteViewModel);
            }
            throw new Exception("Error");
        }
        
        

        [HttpPost]
        public IActionResult Criar(ClienteViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ClienteModel cliente = new ClienteModel(model.Name, model.Email, model.Telefone);
                    ClienteModel created = _clienteRepository.Add(cliente);
                    if (created == null) throw new Exception("Erro ao cadastrar o cliente");
                    TempData["MessageSuccess"] = "Cliente cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }
                throw new Exception("Formulário invalido!");
            }catch (Exception ex)
            {
                TempData["MessageError"] = $"Não foi possivel cadastrar o cliente\n Error:{ex.Message}";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Editar(ClienteViewModel model) {

            try
            {
                if (ModelState.IsValid)
                {
                    ClienteModel cliente = new ClienteModel(model.Name, model.Email, model.Telefone);
                    ClienteModel updated = _clienteRepository.Update(cliente, model.Id);
                    if (updated == null) throw new Exception("Erro ao atualizar o cliente");
                    TempData["MessageSuccess"] = "Cliente alterado com sucesso!";
                    return RedirectToAction("Index");
                }
                throw new Exception("Formulário invalido!");
            }
            catch (Exception ex) {
                TempData["MessageError"] = $"Não foi possivel alterar o cliente\n Error:{ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Delete(ClienteViewModel model) {
            try {
                if (model.Id > 0)
                {
                    bool deleted = _clienteRepository.Delete(model.Id);
                    if (deleted == false) throw new Exception("Delete");
                    TempData["MessageSuccess"] = "Cliente excluindo com sucesso!";
                    return RedirectToAction("Index");
                }
                TempData["MessageError"] = "Não foi possivel deletar o cliente";
                return RedirectToAction("Index");
            }catch(Exception ex) {
                TempData["MessageError"] = $"Não foi possivel deletar o cliente\n Error:{ex.Message}";
                return RedirectToAction("Index");
            }

        }
    }
}
