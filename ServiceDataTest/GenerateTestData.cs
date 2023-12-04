using ServiceData.DatabaseLayer;
using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using Xunit.Abstractions;

namespace ServiceDataTest
{
    public class GenerateTestData
    {

        private readonly ITestOutputHelper _extraOutput;
        private readonly ICustomerGroup _cusGrp;
        private readonly IProduct _product;
        private readonly IShop _shop;
        private readonly ICombo _combo;
        private readonly IDiscount _discount;
        private readonly IIngredient _ingredientAccess;
        private readonly IIngredientProduct _ingredientProduct;
        private readonly IProductGroup _productGroup;
        private readonly IShopProduct _shopProduct;

        //readonly string _connectionString = "Server=localhost; Integrated Security=true; Database=x";
        readonly string _connectionString = "Server=Magnus-PC\\SQLEXPRESS; Integrated Security=true; Database=x";

        public GenerateTestData(ITestOutputHelper output)
        {
            _extraOutput = output;
            _cusGrp = new CustomerGroupDatabaseAccess(_connectionString);
            _product = new ProductDatabaseAccess(_connectionString);
            _shop = new ShopDatabaseAccess(_connectionString);
            _combo = new ComboDatabaseAccess(_connectionString);
            _discount = new DiscountDatabaseAccess(_connectionString);
            _ingredientAccess = new IngredientDatabaseAccess(_connectionString);
            _ingredientProduct = new IngredientProductDatabaseAccess(_connectionString);
            _shopProduct = new ShopProductDatabaseAccess(_connectionString);

        }

        [Fact]
        public void TestGenerateTestData()
        {
            //Create customergroup
            CustomerGroup cusgrp = new CustomerGroup("Vinder");//Creates object
            _cusGrp.CreateCustomerGroup(cusgrp); // Insert into DB

            //Create productgroup
            ProductGroup prodGrp = new ProductGroup("Gruppe 1");
            _productGroup.CreateProductGroup(prodGrp);

            // Create products
            Product prod1 = new Product("11241", "Bøfsandwich", 125, 212141, Product.Category.Burgere, 1, "boefsandwich");
            _product.CreateProduct(prod1);
            Product prod2 = new Product("1231", "Crispy Chicken", 60, 233463, Product.Category.Burgere, 1, "crispychicken");
            _product.CreateProduct(prod2);
            Product prod3 = new Product("1234t6", "Flæskestegsburger", 110, 5675, Product.Category.Burgere, 1, "flaeskestegsburger");
            _product.CreateProduct(prod3);
            Product prod4 = new Product("123asgs", "Cola", 25, 12412521, Product.Category.Drikkevarer, 1, "cola");
            _product.CreateProduct(prod4);
            Product prod5 = new Product("123", "Hamburger", 50, 212141, Product.Category.Burgere, 1, "hamburger");
            _product.CreateProduct(prod5);
            Product prod6 = new Product("1asf12", "Aioli", 5, 246363, Product.Category.Dips, 1, "aioli");
            _product.CreateProduct(prod6);

            // Create shops
            Shop shop1 = new Shop("Jensens Bøfhus", "Boulevarden 2, 9000 Aalborg", Shop.Storetype.Restaurant);
            _shop.CreateShop(shop1);
            Shop shop2 = new Shop("Tages Pølsevogn", "Boulevarden 1, 9000 Aalborg", Shop.Storetype.Restaurant);
            _shop.CreateShop(shop2);
            Shop shop3 = new Shop("Restaurant den fodkolde", "Danmarksgade 10, 9000 Aalborg", Shop.Storetype.Restaurant);
            _shop.CreateShop(shop3);

            // Relate products to shop
            ShopProduct sp1 = new ShopProduct(1, 1);
            _shopProduct.CreateShopProduct(sp1);
            ShopProduct sp2 = new ShopProduct(1,2);
            _shopProduct.CreateShopProduct(sp2);
            ShopProduct sp3 = new ShopProduct(1, 3);
            _shopProduct.CreateShopProduct(sp3);
            ShopProduct sp4 = new ShopProduct(1, 4);
            _shopProduct.CreateShopProduct(sp4);
            ShopProduct sp5 = new ShopProduct(1, 5);
            _shopProduct.CreateShopProduct(sp5);
            ShopProduct sp6 = new ShopProduct(1, 6);
            _shopProduct.CreateShopProduct(sp6);
            ShopProduct sp7 = new ShopProduct(2, 2);
            _shopProduct.CreateShopProduct(sp7);
            ShopProduct sp8 = new ShopProduct(2, 4);
            _shopProduct.CreateShopProduct(sp8);
            ShopProduct sp9 = new ShopProduct(2, 6);
            _shopProduct.CreateShopProduct(sp9);
            ShopProduct sp10 = new ShopProduct(3, 2);
            _shopProduct.CreateShopProduct(sp10);
            ShopProduct sp11 = new ShopProduct(3, 4);
            _shopProduct.CreateShopProduct(sp11);

            //Create ingredients
            Ingredient ing1 = new Ingredient("Bøf", 15, "beef-patty");
            _ingredientAccess.CreateIngredient(ing1);
            Ingredient ing2 = new Ingredient("Salat", 5, "salat");
            _ingredientAccess.CreateIngredient(ing2);
            Ingredient ing3 = new Ingredient("Bacon", 10, "bacon");
            _ingredientAccess.CreateIngredient(ing3);
            Ingredient ing4 = new Ingredient("Løg", 5, "loeg");
            _ingredientAccess.CreateIngredient(ing4);

            //Relate ingredient to products
            IngredientProduct ip1 = new IngredientProduct(1, 1, 1, 2, 1);
            _ingredientProduct.CreateIngredientProduct(ip1);
            IngredientProduct ip2 = new IngredientProduct(2, 2, 0, 1, 1);
            _ingredientProduct.CreateIngredientProduct(ip2);
            IngredientProduct ip3 = new IngredientProduct(3, 2, 0, 1, 1);
            _ingredientProduct.CreateIngredientProduct(ip3);
            IngredientProduct ip4 = new IngredientProduct(4, 4, 0, 1, 1);
            _ingredientProduct.CreateIngredientProduct(ip4);
            IngredientProduct ip5 = new IngredientProduct(4, 1, 0, 1, 1);
            _ingredientProduct.CreateIngredientProduct(ip5);

            //Create combos
            Combo combo1 = new Combo("Hamburger menu", "hamburger-menu", 100);
            _combo.CreateCombo(combo1);
            Combo combo2 = new Combo("Crispy chicken menu", "crispy-chicken-menu", 120);
            _combo.CreateCombo(combo2);
            Combo combo3 = new Combo("Flæskestegsburger m. pommes", "flaeskestegsburger-menu", 160);
            _combo.CreateCombo(combo3);

            //Relate combos to shops
            //Needs to be implemented




         
        }


    }
}

