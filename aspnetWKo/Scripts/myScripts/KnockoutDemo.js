function formatCurrency(value) {
    return "$" + value.toFixed(2);
}
function ProductViewModel() {
    var self = this;
    self.Id = ko.observable("");
    self.Name = ko.observable("");
    self.Category = ko.observable("");
    self.Price = ko.observable("");

    var Product = {
        Id: self.Id,
        Name: self.Name,
        Category: self.Category,
        Price: self.Price
    };

    self.Product = ko.observable();
    self.Products = ko.observableArray();

    $.ajax({
        url: '/Product/GetAllProducts',
        cache: false,
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        data: {},
        success: function (data) {
            self.Products(data);
        }
    });

    self.Total = ko.computed(function () {
        var sum = 0;
        var arr = self.Products();
        for (var i = 0; i < arr.length; i++) {
            sum += arr[i].Price;
        }
        return sum;
    });

    self.create = function () {
        if (Product.Name() !== "" && Product.Price() !== "" && Product.Category() !== "") {
            $.ajax({
                url: '/Product/AddProduct',
                cache: false,
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: ko.toJSON(Product),
                success: function (data) {
                    self.Products.push(data);
                    self.Name("");
                    self.Category("");
                    self.Price("");
                }
            }).fail(function (xhr, textStatus, err) {
                alert(err);
            });
        }
        else {
            alert("Please enter all the values");
        }
    };

    self.delete = function (Product) {
        if (confirm('Are you sure to delete "' + Product.Name + '" product ?')) {
            var id = Product.Id;
            $.ajax({
                url: '/Product/DeleteProduct/' + id,
                cache: false,
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: id,
                success: function (data) {
                    self.Products.remove(Product);
                }
            }).fail(function (xhr, textStatus, err) {
                alert(err);
            });
        }
    };

    self.edit = function (Product) {
        self.Product(Product);
    };

    self.update = function () {
        var Product = self.Product();

        $.ajax({
            url: '/Product/EditProduct/' + Product.Id,
            cache: false,
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON(Product),
            success: function (data) {
                self.Products.removeAll();
                self.Products(data);
                self.Product(null);
                alert("Record updated successfully");
            }
        }).fail(function (xhr, textStatus, err) {
            alert(err);
        });
    };

    self.reset = function () {
        self.Name("");
        self.Price("");
        self.Category("");
    };

    self.cancel = function () {
        self.Product(null);
    };

}

var viewModel = new ProductViewModel();
ko.applyBindings(viewModel);