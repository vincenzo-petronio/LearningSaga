db.auth('admin', 'admin')

db = new Mongo().getDB("saga");

db.createUser({
    'user': "user",
    'pwd': "user",
    'roles': [{
        'role': 'readWrite',
        'db': 'saga'
    }]
});

db.createCollection('products');
db.createCollection('wallet');

db.products.insertMany([
    {
        name: 'phone',
        qty: 5000,
        price: 100
    },
    {
        name: 'keyboard',
        qty: 4000,
        price: 1
    },
    {
        name: 'monitor',
        qty: 20,
        price: 10
    }
]);
