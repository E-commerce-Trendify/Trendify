using Microsoft.EntityFrameworkCore;
using Trendify.DTOs;
using Trendify.Models;
using Trendify.Services;

namespace Test_Tendify
{
    public class UnitTest1 : Mock
    {

        [Fact]
        public async Task CreateProduct()
        {
            // Arrange
            var productService = new ProductsService(_db, null);

            var productDto = new ProductsDto
            {
                Name = "TestProduct",
                Description = "TestDescription",
                Price = 10,
                CategoryID = 1
            };

            // Act
            await productService.Create(productDto, "testImageUrl");

            // Assert
            var createdProduct = await _db.Products.FirstOrDefaultAsync(p => p.Name == "TestProduct");
            Assert.NotNull(createdProduct);
        }

        [Fact]
        public async Task GetAllProducts()
        {
            // Arrange
            var productService = new ProductsService(_db, null);

            // Act
            var products = await productService.GetAllProducts();

            // Assert
            Assert.NotNull(products);
            Assert.True(products.Count > 0);
        }

        [Fact]
        public async Task GetProductById()
        {
            // Arrange
            var productService = new ProductsService(_db, null);

            var productDto = new ProductsDto
            {
                Name = "TestProduct",
                Description = "TestDescription",
                Price = 10,
                CategoryID = 1
            };
            await productService.Create(productDto, "testImageUrl");

            var createdProduct = await _db.Products.FirstOrDefaultAsync(p => p.Name == "TestProduct");

            // Act
            var retrievedProduct = await productService.GetProductById(createdProduct.ProductID);

            // Assert
            Assert.NotNull(retrievedProduct);
            Assert.Equal("TestProduct", retrievedProduct.Name);
        }

        [Fact]
        public async Task UpdateProduct()
        {
            // Arrange
            var productService = new ProductsService(_db, null);

            var productDto = new ProductsDto
            {
                Name = "TestProduct",
                Description = "TestDescription",
                Price = 10,
                CategoryID = 1
            };
            await productService.Create(productDto, "testImageUrl");

            var createdProduct = await _db.Products.FirstOrDefaultAsync(p => p.Name == "TestProduct");

            var updatedProductDto = new ProductsDto
            {
                Name = "UpdatedProductName",
                Description = "UpdatedProductDescription",
                Price = 20,
                CategoryID = 2
            };
            await productService.Update(updatedProductDto, createdProduct.ProductID, "updatedImageUrl");

            // Act
            var updatedProduct = await _db.Products.FirstOrDefaultAsync(p => p.ProductID == createdProduct.ProductID);

            // Assert
            Assert.NotNull(updatedProduct);
            Assert.Equal("UpdatedProductName", updatedProduct.Name);
            Assert.Equal("UpdatedProductDescription", updatedProduct.Description);
            Assert.Equal(20, updatedProduct.Price);
            Assert.Equal(2, updatedProduct.CategoryID);
            Assert.Equal("updatedImageUrl", updatedProduct.ImageUrl);
        }

        [Fact]
        public async Task DeleteProduct()
        {
            // Arrange
            var productService = new ProductsService(_db, null);

            var productDto = new ProductsDto
            {
                Name = "TestProduct",
                Description = "TestDescription",
                Price = 10,
                CategoryID = 1
            };
            await productService.Create(productDto, "testImageUrl");

            var createdProduct = await _db.Products.FirstOrDefaultAsync(p => p.Name == "TestProduct");

            // Act
            await productService.Delete(createdProduct.ProductID);

            // Assert
            var deletedProduct = await _db.Products.FirstOrDefaultAsync(p => p.ProductID == createdProduct.ProductID);
            Assert.Null(deletedProduct);
        }

        [Fact]
        public async Task CreateCategory()
        {
            // Arrange
            var categoryService = new CategoryService(_db, null);

            var categoryDto = new CategoryDTO
            {
                Name = "TestCategory",
                Description = "TestDescription"
            };

            // Act
            await categoryService.Create(categoryDto, "testImageUrl");

            // Assert
            var createdCategory = await _db.Categories.FirstOrDefaultAsync(c => c.Name == "TestCategory");
            Assert.NotNull(createdCategory);
        }


        [Fact]
        public async Task GetAllCategories()
        {
            // Arrange
            var categoryService = new CategoryService(_db, null); 

            // Act
            var categories = await categoryService.GetAllCategories();

            // Assert
            Assert.NotNull(categories);
            Assert.True(categories.Count > 0);
        }

        [Fact]
        public async Task GetCategoryById()
        {
            // Arrange
            var categoryService = new CategoryService(_db, null); 

            var categoryDto = new CategoryDTO
            {
                Name = "TestCategory",
                Description = "TestDescription"
            };
            await categoryService.Create(categoryDto, "testImageUrl");

            // Act
            var createdCategory = await _db.Categories.FirstOrDefaultAsync(c => c.Name == "TestCategory");
            var retrievedCategory = await categoryService.GetCategoryById(createdCategory.CategoryID);

            // Assert
            Assert.NotNull(retrievedCategory);
            Assert.Equal("TestCategory", retrievedCategory.Name);
        }

        [Fact]
        public async Task UpdateCategory()
        {
            // Arrange
            var categoryService = new CategoryService(_db, null); 

            
            var categoryDto = new CategoryDTO
            {
                Name = "TestCategory",
                Description = "TestDescription"
            };
            await categoryService.Create(categoryDto, "testImageUrl");

            var createdCategory = await _db.Categories.FirstOrDefaultAsync(c => c.Name == "TestCategory");

            var updatedCategoryDto = new CategoryDTO
            {
                Name = "UpdatedCategoryName",
                Description = "UpdatedCategoryDescription"
            };
            await categoryService.Update(createdCategory.CategoryID, updatedCategoryDto, "updatedImageUrl");

            // Act
            var updatedCategory = await _db.Categories.FirstOrDefaultAsync(c => c.CategoryID == createdCategory.CategoryID);

            // Assert
            Assert.NotNull(updatedCategory);
            Assert.Equal("UpdatedCategoryName", updatedCategory.Name);
            Assert.Equal("UpdatedCategoryDescription", updatedCategory.Description);
            Assert.Equal("updatedImageUrl", updatedCategory.ImageUrl);
        }

        [Fact]
        public async Task DeleteCategory()
        {
            // Arrange
            var categoryService = new CategoryService(_db, null); 

            
            var categoryDto = new CategoryDTO
            {
                Name = "TestCategory",
                Description = "TestDescription"
            };
            await categoryService.Create(categoryDto, "testImageUrl");

            var createdCategory = await _db.Categories.FirstOrDefaultAsync(c => c.Name == "TestCategory");

            // Act
            await categoryService.Delete(createdCategory.CategoryID);

            // Assert
            var deletedCategory = await _db.Categories.FirstOrDefaultAsync(c => c.CategoryID == createdCategory.CategoryID);
            Assert.Null(deletedCategory);
        }

    }
}