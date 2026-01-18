export const ContactService = {
    getContactsData() {
        return [];
    },
    
    getContactsTestData() {
        return [
            {
                id: '1000',
                name: 'Bamboo Watch',
                email: 'bamboo@watch.com',
                phone: '81987654321',
                isActive: true,
            },
            {
                id: '1001',
                name: 'Black Watch',
                email: 'black@watch.com',
                phone: '82912345678',
                isActive: true,
            },
            {
                id: '1002',
                name: 'Disabled Contact',
                email: 'disabled@email.com',
                phone: '81900000000',
                isActive: false,
            },
        ];
    },

    getProductsMini() {
        return Promise.resolve(this.getProductsData().slice(0, 5));
    },

    getProductsSmall() {
        return Promise.resolve(this.getProductsData().slice(0, 10));
    },

    getContactsSmallTest() {
        return Promise.resolve(this.getContactsTestData().slice(0, 10));
    },

    getProducts() {
        return Promise.resolve(this.getProductsData());
    },

    getProductsWithOrdersSmall() {
        return Promise.resolve(this.getProductsWithOrdersData().slice(0, 10));
    },

    getProductsWithOrders() {
        return Promise.resolve(this.getProductsWithOrdersData());
    }
};
