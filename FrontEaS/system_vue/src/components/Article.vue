<template>
    <CrudTable
        title="Article"
        icon="mdi-book-open-variant"
        :headers="headers"
        :fields="fields"
        :api="api"
        id-field="articleId"
        status-field="isActive"
    />
</template>

<script>
import CrudTable from './CrudTable.vue';

export default {
    components: { CrudTable},
    data(){
        return {
            headers: [
                { title: 'Article Id', key: 'articleId' },
                { title: 'Name', key: 'articleName' },
                { title: 'Description', key: 'articleDescription' },
                { title: 'Active', key: 'isActive' },
                { title: 'Actions' ,key: 'actions' }
            ],
            fields:[
                { model: 'articleName', label: 'Name' },
                { model: 'articleDescription', label: 'Description' },
                { model: 'isActive', label: 'Is Active', type: 'v-switch', default: true}
            ],
            api: {
                list: 'api/Article/Listing',
                create: 'api/Article/Create',
                update: 'api/Article/Update',
                delete: 'api/Article/Delete/${id}'
            }
        }
    }
}
</script>