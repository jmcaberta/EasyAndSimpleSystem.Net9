
<template>
    <v-sheet border rounded>
        <v-data-table :loading="loading" :headers="headers" :items="items" :search="search">
            <template v-slot:top>
                <v-toolbar flat>
                    <v-toolbar-title>
                        <v-icon color="primary" start>{{ icon }}</v-icon>
                    </v-toolbar-title>
                    <v-spacer></v-spacer>
                    <v-row class="align-center" align="center" justify="end">
                        <v-col cols="12" sm="6" md="4">
                            <v-text-field 
                                v-model="search" 
                                prepend-inner-icon="mdi-magnify" 
                                clearable density="compact" 
                                variant="outlined" 
                                placeholder="Search..." 
                                hide-details/>                            
                        </v-col>
                        <v-col cols="12" sm="auto">
                            <v-btn color="primary" prepend-icon="mdi-plus" text="New" @click="openDialog(false)" block/>
                        </v-col>
                    </v-row>
                    
                </v-toolbar>
            </template>

                <template v-slot:[`item.${statusField}`]="{ value }">
                    <v-icon :color="value ? 'green' : 'red'">{{ value ? 'mdi-check-all' : 'mdi-close-outline' }}</v-icon>
                </template>

                <template v-slot:item.actions="{ item }">
                    <v-icon color="blue" icon="mdi-pencil" @click="openDialog(true, item)"></v-icon>
                    <v-icon color="red" icon="mdi-delete" @click="removeItem(item[idField])"></v-icon>
                </template>

                <template v-slot:no-data>
                    <v-btn text="Reload" @click="loadItems">Reload</v-btn>
                </template>            
        </v-data-table>
    </v-sheet>

    <v-dialog v-model="dialog" max-width="500">
        <v-card :title="dialogTitle">
            <v-card-text>                
                <v-row>
                    <v-col v-for="field in fields" :key="field.model" cols="12">
                        <v-switch v-if="field.type === 'switch'" 
                                  v-model="record[field.model]" 
                                  :label="field.label" />
                        <v-select v-else-if="field.type === 'select'"
                                v-model="record[field.model]"
                                :label="field.label"
                                :items="field.items"
                                item-text="title"
                                item-value="value"/>
                        <v-text-field v-else 
                                      v-model="record[field.model]" 
                                      :label="field.label" 
                                      :type="field.type === 'number' ? 'number' : 'text'" />
                    </v-col>
                </v-row>                
            </v-card-text>
        <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn text="Cancel" @click="dialog = false"/>
            <v-btn text="Save" @click="saveItem"/>
        </v-card-actions>
        </v-card>
    </v-dialog>

    <v-snackbar v-model="snackbar" :color="snackbarColor">{{ snackbarText }}</v-snackbar>
</template>

<script>
import axios from 'axios';
export default{
    props: {
        title: String,
        icon: String,
        headers: Array,
        fields: Array,
        api: Object,
        idField: {
            type: String,
            default: 'id'
        },
        statusField:{
            type: String,
            default: 'isActive'
        }
    },
    data() {
        return{
            items: [],
            search: '',
            dialog: false,
            editing: false,
            record: {},
            snackbar: false,
            snackbarText: '',
            snackbarColor: 'success',
            loading: false
        }
    },
    computed: {
        dialogTitle(){
            return this.editing ? `Edit ${this.title}` : `New ${this.title}`
        }
    },
    methods:{
        loadItems(){
            this.loading = true
            axios.get(this.api.list).then(res => {
                this.items = res.data
            }).finally(() => {
                this.loading = false
            })
        },
        openDialog( editing, item = null ){
            console.log('recibido', item)
            this.editing = editing
            this.dialog = true
            if (editing && item) {                
                this.record = JSON.parse(JSON.stringify(item || {}))               
            } else {
                this.record = Object.fromEntries(this.fields.map(f => [f.model, f.default ?? null]))
            }            
        },
        normalizeItem(item){
            const normalized = {}
            this.fields.forEach(f => {
                normalized[f.model] = item[f.model] ?? ''
            })
            return normalized
        },
        saveItem(){
            const req = this.editing
                ? axios.put(this.api.update, this.record)
                : axios.post(this.api.create, this.record)
            req.then(() => {
                this.loadItems()
                this.snackbarText = `${this.title} ${this.editing ? 'update' : 'created'} successfully`
                this.snackbarColor = 'success'
                this.snackbar = true
                this.dialog = false
            }).catch(err => {
                this.snackbarText =`Error: ${err.message}`
                this.snackbarColor = 'error'
                this.snackbar = true
            })
        },
        removeItem(id) {
            if (!confirm('Are you sure?')) return 
            axios.delete(this.api.delete(id)).then(() => {
                this.loadItems()
                this.snackbarText = `${this.title} deleted`
                this.snackbarColor = 'success'
                this.snackbar = true
            }) 
        },
    },
    mounted(){
        this.loadItems()
    }
}
</script>

<style scoped>
v-icon{
    cursor: pointer;
    margin-inline: 4px;
}
</style>
