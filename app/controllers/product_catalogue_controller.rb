class ProductCatalogueController < ApplicationController
  def index
    @products = Product.get_products_for_catalogue
  end
end
