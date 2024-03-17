<script setup>
    import { ProductComponentValue, ProductComponent } from "@/components/productComponent"
    import { PageBannerComponent, BreadcrumbValue } from '@/components/pageBannerComponent';
    import axios from 'axios';
    import { NumberRangeFilter, SearchFilter, RecursiveItemValue, RecursiveTreeFilter, SelectListFilter, SelectListValue } from '@/components/filterBoxComponent';
    import { router_names } from '@/router';
    import { VPagination } from "vuetify/components";
    import { TabsIcon } from "@/components/icons";
    import { useCategoryStore } from "@/stores/CategoryStore";
</script>
<template>
    <PageBannerComponent :breadcrumbList="breadcrumbValues"
        title="Shop">
    </PageBannerComponent>
    <div class="container mb-5 mt-5">
        <div class="row">
            <div class="col-md-3">
                <SearchFilter @on-search="paramSearch" :defaultValue="$route.query.search"></SearchFilter>
                <RecursiveTreeFilter v-model="recursiveItemValues" title="Categories"></RecursiveTreeFilter>
                <NumberRangeFilter @on-search="priceRangeSearch" :defaultMin="parseInt($route.query.min)"
                    :defaultMax="parseInt($route.query.max)" />
                <SelectListFilter title="Sort by" :items="selectListValues" @on-search="sort"
                    :defaultValue="parseInt($route.query.sort) || 0">
                </SelectListFilter>
            </div>
            <div class="col-md-9">
                <div class="row">
                    <template v-if="products && products.length > 0">
                        <ProductComponent v-for="pr in products" :key="pr.id" :value="pr"
                            class="col-sm-6 col-md-4 col-lg-3 mb-5">
                        </ProductComponent>
                    </template>
                    <TabsIcon v-else style="max-height: 500px;" />
                    <v-pagination :model-value="parseInt($route.query.page) || 1" :length="totalPage" :total-visible="7"
                        @update:modelValue="paginationChange"></v-pagination>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
    export default {
        data() {
            return {
                products: [],
                categoryStore:useCategoryStore(),
                recursiveItemValues: [],
                selectListValues: [
                    new SelectListValue({ name: "New to old", value: 0 }),
                    new SelectListValue({ name: "Higher Price", value: 1 }),
                    new SelectListValue({ name: "Lower Price", value: 2 }),
                    new SelectListValue({ name: "Higher Rate", value: 3 })
                ],
                totalPage: 0,
            }
        },
        methods: {
            async getProductList() {
                var res = await axios.get("/Anonym/Product/GetList", { params: { categories: this.categoryStore.childCategories(parseInt(this.$route.params.category)).map(x => x.id), ...this.$route.query, } });
                if (res.isSuccess) {
                    this.products = []
                    this.totalPage = res.data.count / 24;
                    res.data.values.forEach(element => {
                        this.products.push(new ProductComponentValue(element))
                    });
                }
            },
            async getCategories() {
                const dataToRecursiveItemValue = (val) => {
                    if (val.childrens != null && val.childrens.length > 0) {
                        val.childrens = val.childrens.map(dataToRecursiveItemValue)
                    }
                    return new RecursiveItemValue({ id: val.id, name: val.name, link: { name: router_names.shop, params: { category: val.id } }, childrens: val.childrens })
                }
                this.recursiveItemValues = this.categoryStore.values.filter(x => x.parentId == null).map(dataToRecursiveItemValue)

            },
            paramSearch(param) {
                var query = { ...this.$route.query };
                query["search"] = param;
                query.page=1;
                this.$router.push({ query })
            },
            priceRangeSearch(minVal, maxVal) {
                var query = { ...this.$route.query };
                query["min"] = minVal;
                query["max"] = maxVal;
                query.page=1;
                this.$router.push({ query })
            },
            sort(param) {
                var query = { ...this.$route.query };
                query["sort"] = param;
                query.page=1;
                this.$router.push({ query })
            },
            async paginationChange(newPage) {
                var query = { ...this.$route.query };
                query["page"] = newPage;
                this.$router.push({ query })
            }
        },
        async mounted() {
            await this.getCategories();
            await this.getProductList();
        },
        computed: {
            breadcrumbValues() {
                return this.categoryStore.parentCategories(parseInt(this.$route.params.category)).map(x => new BreadcrumbValue({ text: x.name, link: { name: router_names.shop, params: { category: x.id } } }));
            },
        },
        watch: {
            "$route.query"() {
                this.getProductList();
            }
        }
    }
</script>
<style src="@vueform/slider/themes/default.css"></style>
<style scoped>
    .aside_menu {
        border: 1px solid color-mix(in srgb, var(--first-color) 25%, transparent);
        padding-top: 20px;

    }

    svg#freepik_stories-tabs:not(.animated) .animable {
        opacity: 0;
    }

    svg#freepik_stories-tabs.animated #freepik--Window--inject-1--inject-17 {
        animation: 1s 1 forwards cubic-bezier(0.36, -0.01, 0.5, 1.38) slideDown;
        animation-delay: 0.3s;
        opacity: 0;
    }

    svg#freepik_stories-tabs.animated #freepik--Character--inject-1--inject-17 {
        animation: 1s 1 forwards cubic-bezier(0.36, -0.01, 0.5, 1.38) slideDown;
        animation-delay: 0s;
    }

    @keyframes slideDown {
        0% {
            opacity: 0;
            transform: translateY(-30px);
        }

        100% {
            opacity: 1;
            transform: translateY(0);
        }
    }

    .animator-hidden {
        display: none;
    }

</style>