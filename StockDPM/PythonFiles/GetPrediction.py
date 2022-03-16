def InitData():

    # Shape = 14, 500, 5
    ExchangeDataYesterday_X: np.ndarray = np.load("/home/martin/RiderProjects/Lexonic/StockDPM/NPYs/ExchangeStockX.npy", allow_pickle=True).transpose((1, 0, 2))[:-1]
    ExchangeDataToday_X = np.load("/home/martin/RiderProjects/Lexonic/StockDPM/NPYs/ExchangeStockX.npy", allow_pickle=True).transpose((1, 0, 2))[1:]
    # Shape = 14, 500, 25
    ExchangeNewsYesterday_X = np.load("/home/martin/RiderProjects/Lexonic/StockDPM/NPYs/ExchangeNewsX.npy", allow_pickle=True).transpose((1, 0, 2))[:-1]
    ExchangeNewsToday_X = np.load("/home/martin/RiderProjects/Lexonic/StockDPM/NPYs/ExchangeNewsX.npy", allow_pickle=True).transpose((1, 0, 2))[1:]

    ExchangeDataYesterday_X_ForIndex = ExchangeDataYesterday_X.transpose((1, 0, 2))
    ExchangeDataToday_X_ForIndex = ExchangeDataToday_X.transpose((1, 0, 2))
    ExchangeNewsYesterday_X_ForIndex = ExchangeNewsYesterday_X.transpose((1, 0, 2))
    ExchangeNewsToday_X_ForIndex = ExchangeNewsToday_X.transpose((1, 0, 2))

    ExchangeDataYesterday_X = np.tile(ExchangeDataYesterday_X.reshape((14, 500, 5, 1)), 500).reshape((500, 14, 5, 500, 1))
    ExchangeDataToday_X = np.tile(ExchangeDataToday_X.reshape((14, 500, 5, 1)), 500).reshape((500, 14, 5, 500, 1))
    ExchangeNewsYesterday_X = np.tile(ExchangeNewsYesterday_X.reshape((14, 500, 25, 1)), 500).reshape((500, 14, 25, 500,1))
    ExchangeNewsToday_X = np.tile(ExchangeNewsToday_X.reshape((14, 500, 25, 1)), 500).reshape((500, 14, 25, 500, 1))


    Scalar = joblib.load("/home/martin/5yrData/scaler.save")

    ExchangeDataYesterday_X_ForIndex: np.ndarray = Scalar.fit_transform(ExchangeDataYesterday_X_ForIndex.reshape(-1, ExchangeDataYesterday_X_ForIndex.shape[-1])).reshape(ExchangeDataYesterday_X_ForIndex.shape)
    ExchangeDataToday_X_ForIndex = Scalar.fit_transform(ExchangeDataToday_X_ForIndex.reshape(-1, ExchangeDataToday_X_ForIndex.shape[-1])).reshape(ExchangeDataToday_X_ForIndex.shape)
    ExchangeNewsYesterday_X_ForIndex = Scalar.fit_transform(ExchangeNewsYesterday_X_ForIndex.reshape(-1, ExchangeNewsYesterday_X_ForIndex.shape[-1])).reshape(ExchangeNewsYesterday_X_ForIndex.shape)
    ExchangeNewsToday_X_ForIndex = Scalar.fit_transform(ExchangeNewsToday_X_ForIndex.reshape(-1, ExchangeNewsToday_X_ForIndex.shape[-1])).reshape(ExchangeNewsToday_X_ForIndex.shape)

    ExchangeDataYesterday_X = Scalar.fit_transform(ExchangeDataYesterday_X.reshape(-1, ExchangeDataYesterday_X.shape[-1])).reshape(ExchangeDataYesterday_X.shape)
    ExchangeDataToday_X = Scalar.fit_transform(ExchangeDataToday_X.reshape(-1, ExchangeDataToday_X.shape[-1])).reshape(ExchangeDataToday_X.shape)
    ExchangeNewsYesterday_X = Scalar.fit_transform(ExchangeNewsYesterday_X.reshape(-1, ExchangeNewsYesterday_X.shape[-1])).reshape(ExchangeNewsYesterday_X.shape)
    ExchangeNewsToday_X = Scalar.fit_transform(ExchangeNewsToday_X.reshape(-1, ExchangeNewsToday_X.shape[-1])).reshape(ExchangeNewsToday_X.shape)
    print(ExchangeDataToday_X.shape, ExchangeNewsToday_X.shape, ExchangeDataToday_X_ForIndex.shape, ExchangeNewsToday_X_ForIndex.shape)

    Model = keras.models.load_model("/home/martin/5yrData/NewModel.h5")
    Today = Model.predict(x=[
        ExchangeDataToday_X,
        ExchangeDataToday_X_ForIndex,
        ExchangeNewsToday_X,
        ExchangeNewsToday_X_ForIndex])
    np.save("/home/martin/PycharmProjects/Lexonic/tmp_file_npys/Today.npy", Today)

    Yesterday = Model.predict(x=[
        ExchangeDataYesterday_X,
        ExchangeDataYesterday_X_ForIndex,
        ExchangeNewsYesterday_X,
        ExchangeNewsYesterday_X_ForIndex])
    np.save("/home/martin/PycharmProjects/Lexonic/tmp_file_npys/Yesterday.npy", Yesterday)

    Today = np.load("/home/martin/PycharmProjects/Lexonic/tmp_file_npys/Today.npy")
    Yesterday = np.load("/home/martin/PycharmProjects/Lexonic/tmp_file_npys/Yesterday.npy")
    ExpectedDelta = Today/Yesterday * 100
    np.save("/home/martin/PycharmProjects/Lexonic/EndData/EndAll.npy", ExpectedDelta)