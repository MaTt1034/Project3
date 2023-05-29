class Product < ApplicationRecord
  def self.get_products_for_catalogue
    Product.all
  end
end
