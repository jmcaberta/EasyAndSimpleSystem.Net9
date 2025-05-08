<template>
    <CrudTable
        title="Products"
        icon="mdi-package-variant-closed"
        :headers="[
            { title: 'Id', value: 'articleId' },
            { title: 'Category', value: 'categoryName'},
            { title: 'Code', value: 'artCode' },
            { title: 'Name', value: 'artName' },
            { title: 'Price', value: 'sellPrice', inputType: 'number', sortable: false },
            { title: 'No. Items', value: 'itemCount', inputType: 'number', sortable: false},
            { title: 'Description', value: 'artDescription' },
            { title: 'Active', value: 'isActive' },
            { title: 'Actions', value: 'actions', sortable: false }
        ]"
        :fields="[
            { model: 'articleId', label: 'Id'},
            { model: 'catId', label: 'Category', type: 'select', items: categoryOptions },
            { model: 'artCode', label: 'Code' },
            { model: 'artName', label: 'Name' },
            { model: 'sellPrice', label: 'Price' },
            { model: 'itemCount', label: 'No. Items' },
            { model: 'artDescription', label: 'Description'},
            { model: 'isActive', label: 'Active', type: 'switch'}
        ]"
        :api="api"
        id-field="articleId"
        status-field="isActive"
    />
</template>

<script>
import axios from 'axios'
import Category from './Category.vue';
import CrudTable from './CrudTable.vue';

export default {
    components: { CrudTable },
    data(){
        return {
            categoryOptions: [],          
            api: {
                list: 'api/Article/Listing',
                create: 'api/Article/Create',
                update: 'api/Article/Update',
                delete: (id) => `api/Article/Delete/${id}`
            }
        }
    },
    methods: {
        loadCategories() {
            axios.get('/api/Category/Select').then(res => {
                this.categoryOptions = res.data.map(c => ({ title: c.categoryName, value: c.catId}))
            }).catch(err => {
                console.error('Failed to load categories', err)
            })
        }
    },
    mounted() {
        this.loadCategories()
    }
}
</script>