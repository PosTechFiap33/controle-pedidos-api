using ControlePedido.Application.DTOs;
using ControlePedido.Domain.Adapters.Repositories;
using ControlePedido.Domain.Adapters.Services;
using ControlePedido.Domain.Base;
using ControlePedido.Domain.Entities;
using static ControlePedido.Domain.Entities.Pedido;

namespace ControlePedido.Application.UseCases.Pedidos
{
    public interface ICriarPedidoUseCase
    {
        Task<string> Executar(CriarPedidoDTO criarPedidoDTO);
    }

    public class CriarPedidoUseCase : ICriarPedidoUseCase
    {
        private readonly IPedidoRepository _repository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IPagamentoService _service;

        public CriarPedidoUseCase(IPedidoRepository repository,
                                  IPagamentoService service,
                                  IProdutoRepository produtoRepository,
                                  IClienteRepository clienteRepository)
        {
            _repository = repository;
            _service = service;
            _produtoRepository = produtoRepository;
            _clienteRepository = clienteRepository;
        }

        public async Task<string> Executar(CriarPedidoDTO criarPedidoDTO)
        {
            Cliente? cliente = await ConsultarCliente(criarPedidoDTO.ClienteId);

            var itensPedido = new List<PedidoItem>();

            foreach (var itens in criarPedidoDTO.Itens)
            {
                var produto = await _produtoRepository.ConsultarPorId(itens.ProdutoId);

                if (produto is null)
                    throw new DomainException($"Não foi encontrado um pedido com id {itens.ProdutoId}");

                itensPedido.Add(new PedidoItem(produto));
            };

            var pedido = PedidoFactory.Criar(itensPedido, cliente);

            await SalvarPedido(pedido);

            return _service.GerarQRCodePagamento(pedido);
        }

        private async Task<Cliente?> ConsultarCliente(Guid? clienteId)
        {
            if (!clienteId.HasValue)
                return null;

            var cliente = await _clienteRepository.ConsultarPorId(clienteId.Value);

            if (cliente is null)
                throw new DomainException("Não foi localizado um cliente para o id informado!");

            return cliente;
        }

        private async Task SalvarPedido(Pedido pedido)
        {
            _repository.Criar(pedido);
            await _repository.UnitOfWork.Commit();
        }
    }
}

