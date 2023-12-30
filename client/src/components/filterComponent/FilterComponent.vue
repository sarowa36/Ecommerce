<script setup>
import { FilterValueArray, FilterTypeEnum } from "./";
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { Guid } from 'guid-typescript';
import Slider from "@vueform/slider"

</script>
<template>
    <div class="filter" :id="filterId" v-on-click-outside-handler="()=>{ show = false;}">
        <button :class="'btn' + (active ? ' active' : '')" @click="toggleFilter"
            >
            <FontAwesomeIcon :icon="icon"></FontAwesomeIcon>
            <span> {{ describeText }}</span>
        </button>
        <div class="filter_dropdown smooth_shadow" v-if="show">
            <div class="filter_dropdown_body">
                <h5 class="filter_title"> {{ describeText }}</h5>
                <input v-if="filterType != FilterTypeEnum.priceRange && showSearchInput" type="text" placeholder="Search"
                    class="form-control mb-2">
                <ul v-if="filterType != FilterTypeEnum.priceRange">
                    <li v-for="(item) in value"><input type="checkbox" v-model="item.selected"
                            @change="checkboxChanged($event, item)"> {{ item.text }}</li>
                </ul>
                <Slider v-if="filterType == FilterTypeEnum.priceRange" :min="minValue" :max="maxValue"
                    class="mt-5 me-2 ms-2" v-model="priceRangeValue" />
            </div>
            <div class="filter_dropdown_footer">
                <button class="btn btn-primary" @click="_clearEvent">Clear</button>
                <button class="btn btn-outline-primary" @click="_applyEvent">Apply</button>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    data: () => {
        return {
            show: false,
            filterId: Guid.create().toString().replace(/[0-9]/gm, ""),
            minValue: 0,
            maxValue: 0
        }
    },
    mounted() {
        if (this.filterType == FilterTypeEnum.priceRange) {
            this.minValue = this.priceRangeValue[0];
            this.maxValue = this.priceRangeValue[1];
        }
    },
    methods: {
        toggleFilter() {
            this.show = !this.show;
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
                this.value.map(x => {
                    if (this.filterType == FilterTypeEnum.priceRange) {
                    }
                    x.selected = false;
                    return x
                });
            }
        },
        checkboxChanged(e, item) {
            if (this.filterType == FilterTypeEnum.checkboxListOnlyOneSelectable) {
                this.value.filter(x => x != item).map(x => { x.selected = false; return x })
            }
        }
    },
    emits: ["update:modelValue"],
    props: {
        describeText: String,
        icon: String,
        showSearchInput: true,
        modelValue: FilterValueArray,
        filterType: FilterTypeEnum,
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
        },
        priceRangeValue: {
            get() {
                return [this.modelValue[0].value, this.modelValue[1].value];
            },
            set(val) {
                this.value[0].value = val[0];
                this.value[1].value = val[1];
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