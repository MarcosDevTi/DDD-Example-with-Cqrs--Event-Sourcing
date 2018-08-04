using System.Linq;
using DDDExample.Cqrs.Command.Cart;
using DDDExample.Data.Context;
using DDDExample.SharedKernel.Cqrs.Command;

namespace DDDExample.Data.Hanlders.Cart
{
    public class CartCommandHandler :
        ICommandHandler<AddItemCart>
    {
        private readonly DddExampleContext _context;

        public CartCommandHandler(DddExampleContext context)
        {
            _context = context;
        }

        public void Handle(AddItemCart command)
        {
            var orderItem = _context.OrderItems.FirstOrDefault(x => x.Id == command.OrderItemId);
            orderItem.Qtd += command.Value;
            _context.OrderItems.Update(orderItem);
            _context.SaveChanges();
        }
    }
}
