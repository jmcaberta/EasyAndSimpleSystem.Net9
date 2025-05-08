<template>
  <v-sheet border rounded>
    <v-data-table :headers="headers" :items="categories" :search="search">
      <template v-slot:top>
        <v-toolbar flat class="d-flex align-center">
          <v-toolbar-title>
            <v-icon color="primary" icon="mdi-storefront-outline" size="x-small" start></v-icon>
            Category
          </v-toolbar-title>
          <v-spacer></v-spacer>
          <v-text-field class="me-2" v-model="search" label="Search category" prepend-inner-icon="mdi-magnify" clearable
            density="compact" variant="outlined"></v-text-field>
          <v-spacer></v-spacer>
          <v-btn class="me-2" prepend-icon="mdi-plus" rounded="lg" text="New Category" border @click="add"></v-btn>
        </v-toolbar>
      </template>     

      <template v-slot:item.isActive="{ value }">
        <v-icon :color = "value ? 'green': 'red'">
          {{ value ? 'mdi-check-all' : 'mdi-close-outline' }}
        </v-icon>
      </template>
      <template v-slot:item.actions="{ item }">
        <div class="d-flex ga-2 justify-end">
          <v-icon color="medium-emphasis" icon="mdi-pencil" size="small" @click="edit(item.categoryId)"></v-icon>
          <v-icon color="medium-emphasis" icon="mdi-delete" size="small" @click="remove(item.categoryId)"></v-icon>
        </div>
      </template>
      <template v-slot:no-data>
        <v-btn prepend-icon="mdi-backup-restore" rounded="lg" text="Reload from API" variant="text" border
          @click="list"></v-btn>
      </template>
    </v-data-table>
  </v-sheet>

  <v-dialog v-model="dialog" max-width="500">
    <v-card :subtitle="`${isEditing ? 'Update' : 'Create'} a Category`"
      :title="`${isEditing ? 'Edit' : 'Add'} Category`">
      <template v-slot:text>
        <v-row>
          <v-col cols="12" md="8">
            <v-text-field v-model="record.categoryId" label="Category Id"></v-text-field>
          </v-col>
          <v-col cols="12" md="12">
            <v-text-field v-model="record.categoryName" label="Name"></v-text-field>
          </v-col>
          <v-col cols="12" md="12">
            <v-text-field v-model="record.categoryDescription" label="Description"></v-text-field>
          </v-col>
          <v-col cols="12" md="12">
            <v-switch v-model="record.isActive" label="IsActive"></v-switch>
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
  data()
  {
    const adapter = useDate()
    return {
      categories: [],
      adapter,
      DEFAULT_RECORD: { categoryId: null, categoryName: '', categoryDescription: '', isActive: true },
      record: { categoryId: null, categoryName: '', categoryDescription: '', isActive: true },
      dialog: false,
      isEditing: false,
      headers: [
        { title: 'Id', key: 'categoryId', align: 'start' },
        { title: 'Name', key: 'categoryName' },
        { title: 'Description', key: 'categoryDescription', sortable: false },
        { title: 'Is Active', key: 'isActive', align: 'end' },
        { title: 'Actions', key: 'actions', align: 'end', sortable: false },
      ],
      search: '',
      snackbar: false,
      snackbarText: '',
      snackbarColor: 'success',
    }
  },
  mounted()
  {
    this.list()
  },
  methods: {
    list()
    {      
      axios.get('api/Category/Listing').then((response) =>
      {
        this.categories = response.data.map(cat => ({
          ...cat,
          isActive: cat.isActive === true || cat.isActive === 'true'
        }))        
      }).catch((error) =>
      {
        console.log(error)
      });
    },
    add()
    {
      this.isEditing = false
      this.record = { ...this.DEFAULT_RECORD }
      this.dialog = true
    },
    edit(id)
    {
      this.isEditing = true
      const found = this.categories.find(cat => cat.categoryId === id)
      this.record = { 
        ...found,
        isActive: found.isActive === true || found.isActive === 'true'
       }
      this.dialog = true
    },
    remove(id)
    {
      if (window.confirm('Are you sure, you want to delete this category?')){
        axios.delete(`api/Category/Delete/${id}`).then(() => {
          this.list()
          this.snackbarText = 'Category deleted successfully'
          this.snackbarColor = 'success'
          this.snackbar = true
        }).catch(error => {
          console.error(error)
        })
      }      
    },
    save()
    {
      this.record.isActive = this.record.isActive === true || this.record.isActive === 'true'
      if (this.isEditing)
      {
        axios.put('api/Category/Update', this.record).then(()=> {
          this.list();
          this.dialog = false
          this.snackbarText = 'Category updated successfully'
          this.snackbarColor = 'success'
          this.snackbar = true
        }).catch(error => {
          console.error(error)
        })        
      } else
      {
        axios.post('api/Category/Create', this.record).then(() => {
          this.list()
          this.dialog = false
        }).catch(error => {
          console.error(error)
        })        
      }
      this.dialog = false
    },
    /* reset()
    {
      this.dialog = false
      this.record = { ...this.DEFAULT_RECORD }
      this.categories = []
    }, */
  },
}
</script>