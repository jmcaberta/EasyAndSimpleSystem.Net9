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

      <!-- <template v-slot:item.title="{ value }">
        <v-chip :text="value" prepend-icon="mdi-book" label>
          <template v-slot:prepend>
            <v-icon color="primary"></v-icon>
          </template>
        </v-chip>
      </template> -->

      <template v-slot:item.actions="{ item }">
        <div class="d-flex ga-2 justify-end">
          <v-icon color="medium-emphasis" icon="mdi-pencil" size="small" @click="edit(item.CategoryId)"></v-icon>
          <v-icon color="medium-emphasis" icon="mdi-delete" size="small" @click="remove(item.CategoryId)"></v-icon>
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
            <v-text-field v-model="record.CategoryId" label="Category Id"></v-text-field>
          </v-col>

          <v-col cols="12" md="12">
            <v-text-field v-model="record.CategoryName" label="Name"></v-text-field>
          </v-col>
          <v-col cols="12" md="12">
            <v-text-field v-model="record.CategoryDescription" label="Description"></v-text-field>
          </v-col>
          <v-col cols="12" md="12">
            <v-switch v-model="record.IsActive" label="IsActive"></v-switch>
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
      DEFAULT_RECORD: { CategoryId: null, CategoryName: '', CategoryDescription: '', IsActive: true },
      record: { CategoryId: null, CategoryName: '', CategoryDescription: '', IsActive: true },
      dialog: false,
      isEditing: false,
      headers: [
        { title: 'Id', key: 'categoryId', align: 'start' },
        { title: 'Name', key: 'categoryName' },
        { title: 'Description', key: 'categoryDescription', sortable: false },
        { title: 'Is Active', key: 'isActive', align: 'end' },
        { title: 'Actions', key: 'actions', align: 'end', sortable: false },
      ],
      search: ''
    }
  },
  mounted()
  {
    this.list()
  },
  methods: {
    list()
    {
      let me = this;
      axios.get('api/Category/Listing').then(function (response)
      {
        //console.log(response);
        me.categories = response.data;
      }).catch(function (error)
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
      const found = this.categories.find(cat => cat.CategoryId === id)
      this.record = { ...found }
      this.dialog = true
    },
    remove(id)
    {
      const index = this.categories.findIndex(cat => cat.CategoryId === id)
      this.categories.splice(index, 1)
    },
    save()
    {
      if (this.isEditing)
      {
        const index = this.categories.findIndex(cat => cat.CategoryId === this.record.CategoryId)
        this.categories[index] = { ...this.record }
      } else
      {
        this.record.CategoryId = this.categories.length + 1
        this.categories.push({ ...this.record })
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