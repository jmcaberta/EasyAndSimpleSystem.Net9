// Styles
import 'vuetify/styles';
import { md3 } from 'vuetify/blueprints'
import '@mdi/font/css/materialdesignicons.css'
import 'vuetify/styles'

// Vuetify
import { createVuetify } from 'vuetify'

export default createVuetify(
  // https://vuetifyjs.com/en/introduction/why-vuetify/#feature-guides
  {
    blueprint: md3,
    theme: {
      defaultTheme: 'customTheme',
      themes: {
        customTheme: {
          dark: false,
          colors: {
            primary: '#005792', // Azul Profundo - Confianza y Profesionalismo
            secondary: '#F4A261', // Naranja Suave - Complementario sin distracción
            accent: '#2A9D8F', // Verde Agua - Moderno y fresco
            error: '#E63946', // Rojo Coral - Advertencias menos agresivas
            info: '#2196f3',
            success: '#4caf50',
            warning: '#ffc107'
            }
          }
        }
      },
      icons: {
        defaultSet: 'mdi',
      }
    });
