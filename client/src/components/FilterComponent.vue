<script setup>
import FilterValueArray from '../models/FilterValueArray';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { Guid } from 'guid-typescript';
</script>
<template>
    <div class="filter" :id="filterId">
        <button :class="'btn' + (active ? ' active' : '')" @click="toggleFilter">
            <FontAwesomeIcon :icon="icon"></FontAwesomeIcon>
            <span> {{ describeText }}</span>
        </button>
        <div class="filter_dropdown smooth_shadow" v-if="show">
            <div class="filter_dropdown_body">
                <h5 class="filter_title"> {{ describeText }}</h5>
                <input type="text" placeholder="Search" class="form-control mb-2">
                <ul>
                    <li v-for="(item) in value"><input type="checkbox" v-model="item.selected"> {{ item.text }}</li>
                </ul>
            </div>
            <div class="filter_dropdown_footer">
                <button class="btn btn-secondary filter_dropdown_clear" @click="_clearEvent">Clear</button>
                <button class="btn btn-primary filter_dropdown_apply" @click="_applyEvent">Apply</button>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    data: () => {
        return {
            show:false,
            filterId:Guid.create().toString().replace(/[0-9]/gm,"")
        }
    },
    mounted(){
        console.log(this.$el)
        window.addEventListener("click",this.windowClickEvent)
    },
    beforeUnmount(){
        window.removeEventListener("click",this.windowClickEvent)
    },
    methods: {
        toggleFilter() {
           this.show=!this.show;
           
        },
        _applyEvent() {
            if (this.applyEvent) {
                this.applyEvent();
            }
        },
        _clearEvent() {
            if (this.clearEvent) {
                this.clearEvent();
            }
            else {
                this.value.map(x => { x.selected = false; return x });
            }
        },
        windowClickEvent(e){
            var eventNode=e.target;
            if(!eventNode.closest("#"+this.filterId) && this.show){
                this.show=false;
            }
        },
    },
    emits: ["update:modelValue"],
    props: {
        describeText: String,
        icon: String,
        modelValue: FilterValueArray,
        applyEvent: {
            type: Function,
            default: undefined
        },
        clearEvent: {
            type: Function,
            default: undefined
        }
    },
    computed: {
        value: {
            get() {
                return this.modelValue;
            },
            set(val) {
                this.$emit("update:modelValue", val)
            }
        }
    },
}
</script>
<style scoped>
.filter>button {
    display: flex;
    gap: 13px;
    align-items: center;
    color: var(--first-color);
    border: 1px solid var(--second-color);
}

.filter>button:where(:hover, .active) {
    background-color: var(--sixth-color);
}

.filter_dropdown {
    border-radius: 6px;
    width: 370px;
    margin-top: 15px;
    position: absolute;
    background-color: white;
}

.filter_dropdown_body {
    padding: 20px 22px;
}

.filter_dropdown_body ul {
    list-style-type: none;
    padding: 0;
    margin: 0;
    max-height: 155px;
    overflow: scroll;
}

.filter_dropdown_footer {
    background: #EAEAEA;
    display: flex;
    padding: 20px 22px;
    justify-content: space-between;
}

@media (max-width:768px) {
    .filter_list {
        flex-direction: column;
    }

    .filter>.btn {
        width: 100%;
    }

    .filter_dropdown {
        width: 516px;
    }
}

@media (max-width:576px) {
    .filter_dropdown {
        width: calc(100vw - 24px);
    }
}
</style>