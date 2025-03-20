<template>
  <v-layout class="rounded rounded-md border">
    <v-app-bar title="Easy&Simple"></v-app-bar>

    <v-navigation-drawer expand-on-hover rail>
      <v-list color="primary">
        <v-list-item 
        title="Juan Miguel Caberta" 
        prepend-avatar="https://randomuser.me/api/portraits/women/85.jpg"
        subtitle="caber_t@hotmail.com">          
        </v-list-item>
        <v-divider></v-divider>
        <v-list density="compact" nav>
          <v-list-item prepend-icon="mdi-home-city" title="Home" value="home" color="primary" variant="text">        
          </v-list-item> 
          <v-list-item prepend-icon="mdi-account-group-outline" title="Users" value="users" color="primary" variant="text"></v-list-item>         
        </v-list>     
              
        <v-list-group prepend-icon="mdi-storefront" value="store" color="primary">
          <template v-slot:activator="{ props }">
            <v-list-item v-bind="props" title="Store" color="primary" variant="text"></v-list-item>
          </template>
          <v-list-item title="Products" prepend-icon="mdi-package-variant-closed" color="primary" variant="text"></v-list-item>
          <v-list-item title="Sells" prepend-icon="mdi-cash-register" color="primary" variant="text"></v-list-item>
          <v-list-item title="Inventory" prepend-icon="mdi-clipboard-list" color="primary" variant="text"></v-list-item>
        </v-list-group>
      </v-list>
    </v-navigation-drawer>
    
    <v-main class="d-flex align-center justify-center" height="200">
      <v-container>
        <v-sheet
          border="dashed md"
          color="surface-light"
          height="100"
          rounded="lg"
          width="100%"
        ></v-sheet>
      </v-container>
    </v-main>    
  </v-layout>
  <v-container>
  
    <v-sheet
      border="dashed md"
      color="surface-light"
      height="700"
      rounded="lg"
      width="100%"></v-sheet>
  </v-container>
  <v-divider></v-divider>
  <v-footer class="d-flex flex-column" rounded="lg">
    <div class="d-flex w-90 align-center px-4 py-2">
      <strong>Get connected with us on social networks!</strong>

      <div class="d-flex ga-2 ms-auto">
        <v-btn
          v-for="icon in icons"
          :key="icon"
          :icon="icon"
          size="small"
          variant="plain"
        ></v-btn>
      </div>
    </div>

    <div class="px-6 py-2 bg-surface-variant text-center w-100 rounded-lg">
      <strong>Easy&Simple</strong> {{ new Date().getFullYear() }} 
    </div>
  </v-footer>
</template>


<script>

export default {
  name: 'App',

  data: () => ({
    icons: [
        'mdi-facebook',
        'mdi-twitter',
        'mdi-linkedin',
        'mdi-instagram',
      ],
  }),
}
</script>
