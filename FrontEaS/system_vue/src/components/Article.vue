<template>
    <CrudTable
        title="Products"
        icon="mdi-package-variant-closed"
        :headers="[
            { text: 'Id', value: 'artId' },
            { text: 'Category', value: 'categoryName'},
            { text: 'Code', value: 'artCode' },
            { text: 'Name', value: 'artName' },
            { text: 'Price', value: 'sellPrice', inputType: 'number' },
            { text: 'No. Items', value: 'itemCount', inputType: 'number' },
            { text: 'Description', valur: 'artDescription' },
            { text: 'Active', value: 'isActive' },
            { text: 'Actions', value: 'actions', sortable: false }
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
            /* headers: [
                { title: 'Article Id', key: 'articleId' },
                { title: 'Category', key: 'categoryName'},
                { title: 'Article Code', key: 'artCode'},
                { title: 'Name', key: 'artName' },
                { title: 'Price', key: 'sellPrice'},
                { title: 'No. items', key: 'itemCount'},
                { title: 'Description', key: 'artDescription' },
                { title: 'Active', key: 'isActive' },
                { title: 'Actions' ,key: 'actions' }
            ],
            fields:[
                { model: 'articleName', label: 'Name' },
                { model: 'articleDescription', label: 'Description' },
                { model: 'isActive', label: 'Is Active', type: 'v-switch', default: true}
            ], */
            api: {
                list: 'api/Article/Listing',
                create: 'api/Article/Create',
                update: 'api/Article/Update',
                delete: 'api/Article/Delete/${id}'
            }
        }
    },
    methods: {
        loadCategories() {
            axios.get('/api/Category/Listing').then(res => {
                this.categoryOptions = res.data.map(c => ({
                    text: c.categoryName,
                    value: c.categoryId
                }))
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