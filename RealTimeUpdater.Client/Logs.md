ng g c _components/home --skip-tests

ng g c _components/navbar --skip-tests

ng add ngx-bootstrap

ng g c _components/authentication/register --skip-tests
ng g c _components/authentication/login --skip-tests

ng g c _components/misc/input --skip-tests


ng g c _components/user/user-list --skip-tests &&
ng g c _components/user/user-card --skip-tests


ng g c _components/messages --skip-tests 
ng g c _components/chat --skip-tests 

ng g c _components/user-profiles/edit-user --skip-tests


 ng add ngx-bootstrap  --component dropdowns

 ng g c _components/order/order-details --skip-tests
 ng g c _components/order/order-finalise --skip-tests
 ng g c _components/order/view-orders --skip-tests

 ng g c _components/admin/manage-orders --skip-tests

  ng g c _components/admin/manage-roles --skip-tests

  ng add ngx-bootstrap  --component modals


   ng g c _components/misc/orders-modal --skip-tests

ng g s _services/auth --skip-tests


ng g interceptor _interceptors/jwt --skip-tests
