import joblib
import keras.models
import numpy as np
from sklearn.preprocessing import MinMaxScaler
import tensorflow as tf
import pandas as pd
import random


# Using this because the data is already stored in a .pkl format file
df = pd.read_pickle("/home/martin/PycharmProjects/Lexonic/ConvLstmPrediction/Data/Data.pkl")

# Formats Historical Stock Data
stockgroups = [group for _, group in df.groupby(['date'])]
lstnparr = [nparr.drop(['date', 'title', 'SPI_Open', 'SPI_High', 'SPI_Low', 'SPI_Close', 'SPI_Volume', 'stock', 'index'],
                       axis=1).to_numpy() for nparr in stockgroups]
fnparr = np.stack(lstnparr)
HistoricStocksData = fnparr.reshape((fnparr.shape[0], fnparr.shape[1], fnparr.shape[2], 1))

# Formats Historical S&P Data
lstspinx = []
for date in stockgroups:
    lstspinx.append(date.iloc[0].drop(['date', 'title', 'open', 'high', 'low', 'close', 'volume', 'stock', 'index']))
inxparr = np.stack(lstspinx)
HistoricINXData = inxparr.reshape((inxparr.shape[0], inxparr.shape[1], 1))

# Gets News Data Formated
with open('/home/martin/PycharmProjects/Lexonic/ConvLstmPrediction/Data/NewsData.npy', 'rb') as f:
    NewsData = np.load(f, allow_pickle=True)


# For S&P news
inxNews = []
for day in NewsData:
    templist = []
    for stock in day:
        for i in range(25):
            if stock[0][i] != 0.0:
                templist.append(stock[0][i])
    inxNews.append(templist)

for i in range(len(inxNews)):
    random.shuffle(inxNews[i])
    inxNews[i] += [0.0] * (25 - len(inxNews[i]))
    inxNews[i] = np.asarray(inxNews[i][:25]).reshape(25, 1)

DailyINXNewsArr = np.stack(inxNews)


def CreateDataset(dataset, look_back=14):
    dataX, dataY = [], []
    for j in range(len(dataset)-look_back-1):
        a = dataset[j:(j+look_back), 0]
        dataX.append(a)
        dataY.append(dataset[j + look_back, 0])
    return np.array(dataX), np.array(dataY)

# End Result
# print(HistoricStocksData.shape, HistoricINXData.shape, NewsData.shape, DailyINXNewsArr.shape, sep='\n')
# print(HistoricStocksData.shape, HistoricINXData.shape, NewsData.shape, DailyINXNewsArr.shape, EndOfDayResult.shape, sep='\n')

EndOfDayResult_X = np.delete(HistoricINXData[1:], np.s_[-14:], axis=0)
HistoricStocksData = HistoricStocksData[:-1]
HistoricINXData = HistoricINXData[:-1]
NewsData = NewsData[:-1]
DailyINXNewsArr = DailyINXNewsArr[:-1]


HistoricStocksData_X, HistoricINXData_X, NewsData_X, DailyINXNewsArr_X = [], [], [], []

for j in range(99 - 14):
    HistoricStocksData_X.append(HistoricStocksData[j:j+14])
    HistoricINXData_X.append(HistoricINXData[j:j+14])
    NewsData_X.append(NewsData[j:j+14])
    DailyINXNewsArr_X.append(DailyINXNewsArr[j:j+14])


# print(np.stack(HistoricStocksData_X).transpose((0, 1, 3, 2, 4)).shape)
# print(np.stack(HistoricINXData_X).shape)
# print(np.stack(NewsData_X).transpose((0, 1, 4, 2, 3)).shape)
# print(np.stack(DailyINXNewsArr_X).shape)
# print(np.delete(np.squeeze(np.stack(EndOfDayResult_X), axis=-1), [0, 1, 2, 4], axis=2).shape)

ExchangeData_X = np.stack(HistoricStocksData_X).transpose((0, 1, 3, 2, 4)).astype('float32')
IndexData_X = np.stack(HistoricINXData_X).astype('float32')
ExchangeNews_X = np.stack(NewsData_X).transpose((0, 1, 4, 2, 3)).astype('float32')
IndexNews_X = np.stack(DailyINXNewsArr_X).astype('float32')
IndexClose_Y = np.delete(EndOfDayResult_X, [0, 1, 2, 4], axis=1).astype('float32').reshape((EndOfDayResult_X.shape[0], 1))


print(ExchangeData_X.shape, IndexData_X.shape, ExchangeNews_X.shape, IndexNews_X.shape, IndexClose_Y.shape, sep="\n")


np.save("/home/martin/PycharmProjects/Lexonic/ConvLstmPrediction/FinalNPYs/ExchangeData_X.npy", ExchangeData_X)
np.save("/home/martin/PycharmProjects/Lexonic/ConvLstmPrediction/FinalNPYs/IndexData_X.npy", IndexData_X)
np.save("/home/martin/PycharmProjects/Lexonic/ConvLstmPrediction/FinalNPYs/ExchangeNews_X.npy", ExchangeNews_X)
np.save("/home/martin/PycharmProjects/Lexonic/ConvLstmPrediction/FinalNPYs/IndexNews_X.npy", IndexNews_X)
np.save("/home/martin/PycharmProjects/Lexonic/ConvLstmPrediction/FinalNPYs/IndexClose_Y.npy", IndexClose_Y)


ExchangeData_X = np.load("/home/martin/PycharmProjects/Lexonic/ConvLstmPrediction/FinalNPYs/ExchangeData_X.npy",
                         allow_pickle=True)
IndexData_X = np.load("/home/martin/PycharmProjects/Lexonic/ConvLstmPrediction/FinalNPYs/IndexData_X.npy",
                      allow_pickle=True)
ExchangeNews_X = np.load("/home/martin/PycharmProjects/Lexonic/ConvLstmPrediction/FinalNPYs/ExchangeNews_X.npy",
                         allow_pickle=True)
IndexNews_X = np.load("/home/martin/PycharmProjects/Lexonic/ConvLstmPrediction/FinalNPYs/IndexNews_X.npy",
                      allow_pickle=True)
IndexClose_Y = np.load("/home/martin/PycharmProjects/Lexonic/ConvLstmPrediction/FinalNPYs/IndexClose_Y.npy",
                       allow_pickle=True)
print(ExchangeData_X.shape, IndexData_X.shape, ExchangeNews_X.shape, IndexNews_X.shape, IndexClose_Y.shape, sep="\n")

Scalar = MinMaxScaler(feature_range=(0, 1))

ExchangeData_X: np.ndarray = Scalar.fit_transform(ExchangeData_X.reshape(-1, ExchangeData_X.shape[-1])).reshape(
    ExchangeData_X.shape)
IndexData_X = Scalar.fit_transform(IndexData_X.reshape(-1, IndexData_X.shape[-1])).reshape(IndexData_X.shape)
ExchangeNews_X = Scalar.fit_transform(ExchangeNews_X.reshape(-1, ExchangeNews_X.shape[-1])).reshape(
    ExchangeNews_X.shape)
IndexNews_X = Scalar.fit_transform(IndexNews_X.reshape(-1, IndexNews_X.shape[-1])).reshape(IndexNews_X.shape)
IndexClose_Y = Scalar.fit_transform(IndexClose_Y.reshape(-1, IndexClose_Y.shape[-1])).reshape(IndexClose_Y.shape)

print(ExchangeData_X.shape, IndexData_X.shape, ExchangeNews_X.shape, IndexNews_X.shape, IndexClose_Y.shape, sep="\n")

Model = keras.models.load_model("/home/martin/5yrData/my_model.h5")
x = Model.predict(x=[
    ExchangeData_X[-1].reshape(1, ExchangeData_X.shape[1], ExchangeData_X.shape[2], ExchangeData_X.shape[3],
                               ExchangeData_X.shape[4]),
    IndexData_X[-1].reshape(1, IndexData_X.shape[1], IndexData_X.shape[2], IndexData_X.shape[3]),
    ExchangeNews_X[-1].reshape(1, ExchangeNews_X.shape[1], ExchangeNews_X.shape[2], ExchangeNews_X.shape[3],
                               ExchangeNews_X.shape[4]),
    IndexNews_X[-1].reshape(1, IndexNews_X.shape[1], IndexNews_X.shape[2], IndexNews_X.shape[3])])


tstscaler = joblib.load("/home/martin/5yrData/scaler.save")
print(x)
print(type(x))
print(Scalar.inverse_transform(x))
print(tstscaler.inverse_transform(x))
