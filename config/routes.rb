Rails.application.routes.draw do
  resources :customers
  resources :users
  resources :products
  # Define your application routes per the DSL in https://guides.rubyonrails.org/routing.html

  # Defines the root path route ("/")
  root "product_catalogue#index"
end
