<template>
    <v-sheet border rounded>
        <v-data-table :loading="loading" :headers="headers" :items="articles" :search="search">
            <template v-slot:top>
                <v-toolbar flat class="d-flex align-center">
                    <v-toolbar-title>
                        <v-icon color="primary" icon="mdi-package-variant-closed"></v-icon>
                        Products
                    </v-toolbar-title>
                    <v-spacer></v-spacer>
                    <v-text-field class="me-2" v-model="search" label="Search product" prepend-inner-icon="mdi-magnify" clearable density="compact" variant="outlined"></v-text-field>
                    <v-spacer></v-spacer>
                    <v-btn class="me-2" prepend-icon="mdi-plus" rounded="lg" text="New product" border @click="add"></v-btn>
                </v-toolbar>
            </template>

            <template v-slot:item.isActive="{ value }">
                <v-icon :color="value ? 'green' : 'red'">
                    {{ value ? 'mdi-check-all' : 'mdi-close-outline' }}
                </v-icon>
            </template>

            <template v-slot:item.actions="{ item }">
                <div class="d-flex ga-2 justify-end">
                    <v-icon color="blue" icon="mdi-pencil" size="small" @click="edit(item.articleId)"></v-icon>
                    <v-icon color="red" icon="mdi-delete" size="small" @click="remove(item.articleId)"></v-icon>
                </div>
            </template>

            <template v-slot:no-data>
                <v-btn prepend-icon="mdi-backup-restore" rounded="lg" text="Reload" variant="text" border @click="list"></v-btn>
            </template>
        </v-data-table>
    </v-sheet>

    <v-dialog v-model="dialog" max-width="500">
        <v-card :subtitle="`${isEditing ? 'Update' : 'Create'} a product`" :title="`${isEditing ? 'Edit' : 'Add'} Product`">
            <template v-slot:text>
                <v-row>
                    <v-col cols="6" md="6">
                        <v-text-field v-model="record.articleId" label="Product Id"></v-text-field>
                    </v-col>
                    <v-col cols="6" md="6">
                        <v-select v-model="record.catId" :items="category" item-title="text" item-value="value" label="Select category"></v-select>
                    </v-col>
                    <v-col cols="8" md="6">
                        <v-text-field v-model="record.artCode" label="Product code"></v-text-field>
                    </v-col>
                    <v-col cols="12" md="12">
                        <v-text-field v-model="record.artName" label="Name"></v-text-field>
                    </v-col>
                    <v-col cols="6" md="6">
                        <v-text-field type="number" v-model="record.sellPrice" label="Price"></v-text-field>
                    </v-col>
                    <v-col cols="6" md="6">
                        <v-text-field type="number" v-model="record.itemCount" label="Stock"></v-text-field>
                    </v-col> 
                    <v-col cols="12" md="12">
                        <v-text-field v-model="record.artDescription" label="Description"></v-text-field>
                    </v-col>
                    <v-col cols="8" md="6">
                        <v-switch v-model="record.isActive" label="isActive"></v-switch>
                    </v-col>                       
                </v-row>
            </template>

            <v-divider></v-divider>

            <v-card-actions class="bg-surface-light">
                <v-btn text="Cancel" variant="plain" @click="dialog = false"></v-btn>
                <v-spacer></v-spacer>
                <v-btn text="Save" @click="save"></v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
    <v-snackbar v-model="snackbar" :color="snackbarColor" timeout="3000">
        {{ snackbarText }}
    </v-snackbar>
    <v-alert v-if="validateMessage.length" type="error" dense>
        <div v-for="(msg, index) in validateMessage" :key="index"> {{ msg }}</div>
    </v-alert>
</template>
<script>
import { ref } from 'vue'
import axios from 'axios'

export default {
    data(){
        return{
            articles: [],
            DEFAULT_RECORD: { articleId: null, catId: null, artCode: '', artName: '', sellPrice: null, itemCount: null, artDescription: '', isActive: true },
            record: { articleId: null, catId: null, artCode: '', artName: '', sellPrice: null, itemCount: null, artDescription: '', isActive: true },
            dialog: false,            
            headers: [
                { title: 'Id', value: 'articleId' },
                { title: 'Category', value: 'categoryName'},
                { title: 'Code', value: 'artCode' },
                { title: 'Name', value: 'artName' },
                { title: 'Price', value: 'sellPrice', inputType: 'number', sortable: false },
                { title: 'No. Items', value: 'itemCount', inputType: 'number', sortable: false},
                { title: 'Description', value: 'artDescription' },
                { title: 'Active', value: 'isActive' },
                { title: 'Actions', value: 'actions', sortable: false }
            ],
            isEditing: false,
            search: '',
            snackbar: false,
            snackbarText: '',
            snackbarColor: 'success',
            loading: false,
            category: [],
            valida: 0,
            validateMessage: []
        }
    },
    mounted() {
        this.list()
        this.loadCategories()
    },
    methods: {
        list() {
            axios.get('api/Article/Listing').then((response) => {
                this.articles = response.data.map(art => ({
                    ...art,                    
                    isActive: Boolean(art.isActive) //isActive: art.isActive === true || art.isActive === 'true'
                }))
            }).catch((error) => {
                console.log(error)
            })
        },
        add() {
            this.isEditing = false
            this.record = { ...this.DEFAULT_RECORD }
            this.dialog = true
        },
        edit(id) {
            this.isEditing = true
            const found = this.articles.find(art => art.articleId === id)
            if (!found) {
                this.snackbarText = 'Product not found'
                this.snackbarColor = 'error'
                this.snackbar = true
                return
            }
            this.record = {
                ...found, isActive: Boolean(found.isActive) //isActive: found.isActive === true || found.isActive === 'true'
            }
            this.dialog = true
        },
        remove(id) {
            if (window.confirm('Are you sure, you want to delete this product?')){
                axios.delete(`api/Article/Delete/${id}`).then(() => {
                    this.list()
                    this.snackbarText = 'Product deleted successfully'
                    this.snackbarColor = 'success'
                    this.snackbar = true                    
                })
            }
        },
        save() {
            if (this.validate()){
                this.snackbarText = this.validateMessage.join('\n')
                this.snackbarColor = 'error'
                this.snackbar = true
                return
            }
            this.record.isActive = Boolean(this.record.isActive) //this.record.isActive = this.record.isActive === true || this.record.isActive === 'true'
            const request = this.isEditing
                ? axios.put('api/Article/Update', this.record)
                : axios.post('api/Article/Create', this.record)

            request.then(() => {
                this.list()
                this.dialog = false
                this.snackbarText = this.isEditing ? 'Product updated successfully' : 'Product created successfully'
                this.snackbarColor = 'success'
                this.snackbar = true
            }).catch(error => {
                console.log(error)
                this.snackbarText = 'Error saving product'
                this.snackbarColor = 'error'
                this.snackbar = true
            })
        },
        loadCategories(){            
            axios.get('api/Category/Select').then(response => {
                this.category = response.data.map(c => ({
                    text: c.categoryName,
                    value: c.catId
                }))
            })               
            .catch(function(error){
                console.log(error)
            })
        },
        validate() {
            this.valida = 0
            this.validateMessage = []

            if (this.record.artName.length < 5 || this.record.artName.length > 50) {
                this.validateMessage.push("The product name must be longer than 5 characters and shorter than 50 characters.")
            }
            if (!this.record.catId) {
                this.validateMessage.push("Select a category.")
            }
            if (!this.record.itemCount || this.record.itemCount == 0) {
                this.validateMessage.push("Stock quantity is required.")
            }
            if (!this.record.sellPrice || this.record.sellPrice == 0) {
                this.validateMessage.push("The price must be greater than zero.")
            }
            if  (this.validateMessage.length) {
                this.valida = 1
            }
            return this.valida
        }       
    }
}
</script>

<!-- <template>
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
            { model: 'catId', label: 'Category', type: 'select', items: categoryOptions, default: null },
            { model: 'artCode', label: 'Code' },
            { model: 'artName', label: 'Name' },
            { model: 'sellPrice', label: 'Price', type: 'number' },
            { model: 'itemCount', label: 'No. Items', type: 'number' },
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
        },        
    },
    mounted() {
        this.loadCategories()
    }
}
</script> -->