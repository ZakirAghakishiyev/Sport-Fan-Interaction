document.addEventListener('click', (e) => {
    const link = e.target.closest('a');
    if (
        link &&
        link.href &&
        !link.href.startsWith('javascript:') &&
        !link.href.startsWith('#') &&
        link.getAttribute('href') !== '#'
    ) {
        window.app.showPageLoading();
    }
});

// --- Demo Products for İmişli FK Merchandise ---
const DEMO_PRODUCTS = [
  {
    id: 1,
    name: "İmişli FK Home Jersey",
    description: "Official 2024/25 home kit. Show your club pride!",
    price: 49.99,
    category: "jerseys",
    imageUrl: "img/image.png"
  },
  {
    id: 2,
    name: "İmişli FK Scarf",
    description: "Warm, stylish scarf in club colors.",
    price: 14.99,
    category: "accessories",
    imageUrl: "img/image.png"
  },
  {
    id: 3,
    name: "İmişli FK Cap",
    description: "Classic cap with embroidered club logo.",
    price: 19.99,
    category: "accessories",
    imageUrl: "img/image.png"
  },
  {
    id: 4,
    name: "İmişli FK Away Jersey",
    description: "Official 2024/25 away kit. Limited edition!",
    price: 54.99,
    category: "jerseys",
    imageUrl: "img/image.png"
  },
  {
    id: 5,
    name: "İmişli FK Mug",
    description: "Ceramic mug with club crest. Perfect for fans!",
    price: 9.99,
    category: "accessories",
    imageUrl: "img/image.png"
  }
];

function renderProducts(products) {
  const grid = document.getElementById('productsGrid');
  if (!grid) return;
  grid.innerHTML = '';
  if (!products || products.length === 0) {
    grid.innerHTML = '<p class="no-items">No products available at the moment.</p>';
    return;
  }
  products.forEach(product => {
    const card = document.createElement('div');
    card.className = 'product-card';
    card.innerHTML = `
      <div class="product-image" style="background: var(--primary-color); display: flex; align-items: center; justify-content: center;">
        <img src="${product.imageUrl}" alt="${product.name}" style="width: 80px; height: 80px; object-fit: contain; background: #fff; border-radius: 10px; padding: 8px;" />
      </div>
      <div class="product-content">
        <h3 class="product-title">${product.name}</h3>
        <p class="product-price">$${product.price.toFixed(2)}</p>
        <p>${product.description}</p>
        <button class="btn btn-primary add-to-cart-btn" data-product-id="${product.id}">Add to Cart</button>
      </div>
    `;
    grid.appendChild(card);
  });
}

document.addEventListener('DOMContentLoaded', () => {
  if (window.location.pathname.endsWith('merchandise.html')) {
    renderProducts(DEMO_PRODUCTS);
  }
});