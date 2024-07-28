<script setup>
import { DashboardLayout } from "@/components/dashboard/"
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome"
</script>
<template>
    <DashboardLayout>
        <div class="row align-items-center ps-3 pe-3 pt-5">
            <div class="col-md-6">
                <div class="font-weight-bold font-xl">meine list</div>
                <div class="font-size-s">5 product</div>
            </div>
            <div class="col-md-6 savelist_actions">
                <button class="btn btn-outline-primary" click="CreateAddress">Edit List <FontAwesomeIcon class="ms-1"
                        icon="pen"></FontAwesomeIcon></button>
                <button class="btn btn-outline-danger" click="CreateAddress">Delete List <FontAwesomeIcon class="ms-1"
                        icon="trash"></FontAwesomeIcon></button>
            </div>
            <hr class="mt-3 mb-4">
        </div>
        <div class="row ps-3 pe-3">
            <div class="col-12 mb-4 save_product">
                <img src="/Admin/Product/ffcab2c3-8d8f-4695-bf83-e1ce3a372e6a/seashell.webp" class="save_product_img"
                    alt="">
                <div class="save_product_name font-lg">Product</div>
                <div class="save_product_price">17.99$</div>
                <div class="save_product_actions"> <button class="btn btn-outline-primary">Go Product</button><button
                        class="btn btn-danger">Delete Product</button></div>
            </div>
            <div class="col-12 mb-3 save_product">
                <img src="/Admin/Product/ffcab2c3-8d8f-4695-bf83-e1ce3a372e6a/seashell.webp" class="save_product_img"
                    alt="">
                <div class="save_product_name font-lg">Product</div>
                <div class="save_product_price">17.99$</div>
                <div class="save_product_actions"> <button class="btn btn-outline-primary">Go Product</button><button
                        class="btn btn-danger">Delete Product</button></div>
            </div>
        </div>
    </DashboardLayout>
</template>
<style scoped>
.save_product {
    display: grid;
    grid-template-columns: 20% repeat(3, 25%);
    justify-content: space-between;
}

.save_product_actions {
    padding: 10px;
    display: flex;
    flex-wrap: wrap;
    align-content: center;
    gap: 15px;
}

.save_product_actions>button {
    width: 100%;
}

.save_product_img {
    aspect-ratio: 1;
    width: 100%;
    object-fit: cover;
}

.save_product_name,
.save_product_price {
    padding: 15px 10px;
}
.savelist_actions{
    column-gap: 15px;
    row-gap: 8px;
  display: flex;
  justify-content: end;
}
@media (max-width:768px) {
    .save_product{
        grid-template-columns: 100px auto;
        justify-content: initial;
    }
    .save_product_img{
        grid-row: 1/span 2;
    }
    .save_product_name{
        grid-column:2;
    }
    .save_product_price{
        grid-column: 2;
    }
    .save_product_actions{
        grid-row: 4;
grid-column: 1/span 2;
    }
    .savelist_actions {
        display: grid;
        grid-template-columns: repeat(2,calc(50% - 7.5px));
    }
}
</style>
<script>
export default {
}
</script>
