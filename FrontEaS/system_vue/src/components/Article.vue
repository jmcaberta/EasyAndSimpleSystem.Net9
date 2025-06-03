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
                    <v-col cols="8" md="6">
                        <v-text-field v-model="record.articleId" label="Product Id"></v-text-field>
                    </v-col>
                    <v-col cols="12" md="8">
                        <v-text-field v-model="record.catId" label="Category"></v-text-field>
                    </v-col>
                    <v-col cols="8" md="6">
                        <v-text-field v-model="record.artCode" label="Product code"></v-text-field>
                    </v-col>
                    <v-col cols="12" md="8">
                        <v-text-field v-model="record.artName" label="Name"></v-text-field>
                    </v-col>
                    <v-col cols="6" md="4">
                        <v-text-field v-model="record.sellPrice" label="Price"></v-text-field>
                    </v-col>
                    <v-col cols="6" md="4">
                        <v-text-field v-model="record.itemCount" label="Stock"></v-text-field>
                    </v-col> 
                    <v-col cols="12" md="12">
                        <v-text-field v-model="record.artDescription" label="Description"></v-text-field>
                    </v-col>
                    <v-col cols="8" md="6">
                        <v-switch v-model="record.isActive" label="Active"></v-switch>
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
</template>
<script>
import { onMounted, ref, shallowRef } from 'vue'
import { useDate } from 'vuetify'
import axios from 'axios'

export default {
    data(){
        const adapter = useDate()
        return{
            articles: [],
            adapter,
            DEFAULT_RECORD: { articleId: null, catId: null, artCode: '', artName: '', sellPrice: null, itemCount: null, artDescription: '', isActive: true },
            record: { articleId: null, catId: null, artCode: '', artName: '', sellPrice: null, itemCount: null, artDescription: '', isActive: true },
            dialog: false,
            isEditing: false,
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
            search: '',
            snackbar: false,
            snackbarText: '',
            snackbarColor: 'success',
            loading: false
        }
    },
    mounted() {
        this.list()
    },
    methods: {
        list() {
            axios.get('api/Article/Listing').then((response) => {
                this.articles = response.data.map(art => ({
                    ...art,
                    isActive: art.isActive === true || art.isActive === 'true'
                }))
            }).catch((error) => {
                console.log(error)
            })
        },
        add() {
            this.isActive = false
            this.record = { ...this.DEFAULT_RECORD }
            this.dialog = true
        },
        edit(id) {
            this.isEditing = true
            const found = this.articles.find(art => art.articleId === id)
            this.record = {
                ...found,
                isActive: found.isActive === true || found.isActive === 'true'
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
            this.record.isActive = this.record.isActive === true || this.record.isActive === 'true'
            if (this.isEditing) {
                axios.put('api/Article/Update', this.record).then(() => {
                    this.list()
                    this.dialog = false
                    this.snackbarText = 'Product updated successfully'
                    this.snackbarColor = 'success'
                    this.snackbar = true
                }).catch(error => {
                    console.log(error)
                })
            } else {
                axios.post('api/Article/Create', this.record).then(() => {
                    this.list()
                    this.dialog = false                    
                }).catch(error => {
                    console.log(error)
                })
            }
            this.dialog = false
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