// mixins.js
import Vue from 'vue'
import Component from 'vue-class-component'

@Component
export class AuthMixin extends Vue {
    get currentUser() {
        return this.$store.state.authModule.user;
    }

    get isCurrentUserAdmin() {
        return this.$store.getters.isCurrentUserAdmin;
    }

    logout(){
        this.$buefy.dialog.confirm({
            title: 'Logout',
            message: 'Are you sure you want to <b>logout</b> your account?',
            confirmText: 'Logout',
            type: 'is-danger',
            hasIcon: true,
            onConfirm: () => this.confirmLogout()
        })
    }

    confirmLogout(){
        this.$buefy.toast.open('Account logged out!')
    }
}