using System;
using System.Linq;
using Shouldly;
using Xunit;
using YetAnotherECommerce.Modules.Carts.Core.Entities;

namespace YetAnotherECommerce.Modules.Carts.UnitTests.Entities;

public class CartTests
{
    [Fact]
    public void AddItem_WhenCartDoesNotContainItem_ThenShouldAddNewItemToCart()
    {
        var cart = new Cart();
        var cartItem = new CartItem(Guid.NewGuid(), "Ultrabook", 1, 10);

        cart.AddItem(cartItem);

        cart.Items.Count.ShouldBe(1);
        cart.Total.ShouldBe(cartItem.TotalPrice);
    }

    [Fact]
    public void AddItem_WhenCartAlreadyContainItem_ThenShouldIncreateItemQuantity()
    {
        var cart = new Cart();
        var cartItem = new CartItem(Guid.NewGuid(), "Ultrabook", 1, 10);
        cart.AddItem(cartItem);

        cart.AddItem(cartItem);

        cart.Items.Count.ShouldBe(1);
        cart.Items.First().Quantity.ShouldBe(2);
        cart.Total.ShouldBe(2 * cartItem.UnitPrice);
    }

    [Fact]
    public void AddItem_WhenCartAlreadyContainItemWithOutdatedPrice_ThenShouldIncreaseItemQuantityAndUpdateUnitPrice()
    {
        var cart = new Cart();
        var outdatedItem = new CartItem(Guid.NewGuid(), "Ultrabook", 1, 10);
        var updatedItem = new CartItem(outdatedItem.ProductId, outdatedItem.Name, 1, 15);
        cart.AddItem(outdatedItem);

        cart.AddItem(updatedItem);

        cart.Items.Count.ShouldBe(1);
        cart.Items.First().Quantity.ShouldBe(2);
        cart.Total.ShouldBe(2 * updatedItem.UnitPrice);
    }

    [Fact]
    public void RemoveItem_WhenCartHasItem_ThenShouldRemoveItemFromCart()
    {
        var cart = new Cart();
        var cartItem = new CartItem(Guid.NewGuid(), "Ultrabook", 1, 10);

        cart.RemoveItem(cartItem.Id);

        cart.Items.ShouldBeEmpty();
    }
}